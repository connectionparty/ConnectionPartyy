using AutoMapper;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models;
using MVCPresentationLayer.Models.Evento;
using Service.Interfaces;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class EventoController : Controller
    {
        private readonly IEventoService _eventoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ITagService _tagService;


        public EventoController(IEventoService eventoService, ITagService tagService, IUsuarioService usuarioService, IMapper mapper, IHostingEnvironment appEnvironment)
        {
            this._eventoService = eventoService;
            this._usuarioService = usuarioService;
            this._mapper = mapper;
            this._appEnvironment = appEnvironment;
            this._tagService = tagService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            DataResponse<Evento> eventos = await this._eventoService.LerEventosPreferencia(int.Parse(HttpContext.User.Claims.ToList()[2].Value));
            return View(eventos.Data);
        }

        [Authorize]
        public async Task<IActionResult> Register()
        {
            DataResponse<Tags> tags = await this._tagService.GetAll();
            ViewBag.Tags = tags.Data;

            DataResponse<Usuario> usuarios = await this._usuarioService.LerUsuarios();
            ViewBag.Usuarios = usuarios.Data;

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(EventoInsertViewModel viewModel)
        {
            Evento evento = _mapper.Map<Evento>(viewModel);

            int id = int.Parse(HttpContext.User.Claims.ToList()[2].Value);
            SingleResponse<Usuario> responseUsuario = await _usuarioService.GetByID(id);

            string[] tags = this.Request.Form["Tags"].ToString().Split(',');
            string[] usuarios = this.Request.Form["Participantes"].ToString().Split(',');
            string[] documento = this.Request.Form["PrecisaDocumento"].ToString().Split(',');

            if (responseUsuario.Success)
            {
                evento.UsuarioID = responseUsuario.Item.ID;
            }

            for (int i = 0; i <= viewModel.Arquivo.Count; i++)
            {
                var foto = viewModel.Arquivo[i];
                if (foto == null || foto.Length == 0 || !FileHelper.IsValidExtension(foto.FileName))
                {
                    ViewBag.Error = "Insira pelo menos uma imagem. Extensões aceitas: .jpg, .gif, .jpeg ou .png.";
                    return await Register();
                }
            }

            evento.Participantes = new List<Usuario>();
            foreach (var item in usuarios)
            {
                evento.Participantes.Add(new Usuario()
                {
                    ID = int.Parse(item)
                });
            }

            evento.Tags = new List<Tags>();
            foreach (var item in tags)
            {
                evento.Tags.Add(new Tags()
                {
                    ID = int.Parse(item)
                });
            }

            Response response = await _eventoService.Cadastrar(evento);
            if (!response.Success)
            {
                ViewBag.Error = response.Mensagem;
                return await Register();
            }

            //< obtém o caminho físico da pasta wwwroot >
            string caminho_WebRoot = _appEnvironment.WebRootPath;
            string idEvento = evento.ID.ToString();
            string fullFileName = caminho_WebRoot + FileHelper.EVENTO_DIRECTORY + idEvento;

            Directory.CreateDirectory(fullFileName);

            //using (FileStream stream = new FileStream(fullFileName + "\\1.jpg", FileMode.Create))
            //{
            //    await viewModel.Arquivo1.CopyToAsync(stream);
            //}
            //using (FileStream stream = new FileStream(fullFileName + "\\2.jpg", FileMode.Create))
            //{
            //    await viewModel.Arquivo2.CopyToAsync(stream);
            //}
            //using (FileStream stream = new FileStream(fullFileName + "\\3.jpg", FileMode.Create))
            //{
            //    await viewModel.Arquivo3.CopyToAsync(stream);
            //}
            return await Register();
        }
        

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return await this.Index();
            }
            SingleResponse<Evento> eventoResponse = await _eventoService.GetByID(id.Value);
            if (!eventoResponse.Success)
            {
                return RedirectToAction("Index");
            }

            return View(eventoResponse.Item);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(EventoSubscription viewModel)
        {
            if (viewModel.ID == 0)
            {
                return await this.Index();
            }

            int idUser = int.Parse(HttpContext.User.Claims.ToList()[2].Value);
            Response response = await _eventoService.CheckInUsuario(viewModel.ID, idUser);

            return RedirectToAction("Index");
        }

        //O CODIGO ABAIXO FOI OQUE ENTENDEMOS DA INSTRUÇÃO DO SR. CELO MIYAGI :)
        //Possibilade de chamar o metodo do service???
        //Como ele vai cair no controller?
        //Caiu no controller mas id retorna nulo


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Curtir(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Response response = await _eventoService.Curtir(id.Value);
            if (!response.Success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ActionName("Descurtir")]
        public async Task<IActionResult> Descurtir(int? id)
        {
            return null;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Comentario(EventoComentarioViewModel viewModel)
        {
            if (viewModel.ID == 0)
            {
                return RedirectToAction("Index");
            }

            int id = int.Parse(HttpContext.User.Claims.ToList()[2].Value);
            SingleResponse<Usuario> responseUsuario = await _usuarioService.GetByID(id);
            if (!responseUsuario.Success)
            {
                return RedirectToAction("/Usuario/Login");
            }

            Comentario comentario = new Comentario();
            comentario.UsuarioID = responseUsuario.Item.ID;
            comentario.EventoID = viewModel.ID;
            comentario.Texto = viewModel.Comentario;

            Response response = await _eventoService.Comentar(comentario);
            if (!response.Success)
            {
                ViewBag.Errors = response.Mensagem;
            }
            return RedirectToAction("Index");


            //Comentario comentario = new Comentario();
            //SingleResponse<Evento> eventoResponse = await _eventoService.GetByID(id.Value);

            //if (!eventoResponse.Success)
            //{
            //    return RedirectToAction("Index");
            //}

            //int idUsuario = int.Parse(HttpContext.User.Claims.ToList()[2].Value);
            //SingleResponse<Usuario> responseUsuario = await _usuarioService.GetByID(idUsuario);

            //if (!responseUsuario.Success)
            //{
            //    return RedirectToAction("/Usuario/Login");
            //}

            //comentario.UsuarioID = responseUsuario.Item.ID;
            //comentario.EventoID = eventoResponse.Item.ID;

            //Response response = await _eventoService.Comentar(idUsuario, id.Value);
            //if (!response.Success)
            //{
            //    return RedirectToAction("Index");
            //}

        }

    }
}
