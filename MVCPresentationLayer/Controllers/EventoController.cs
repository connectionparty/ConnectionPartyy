using AutoMapper;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models;
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


            if (responseUsuario.Success)
            {
                evento.UsuarioID = responseUsuario.Item.ID;
            }

            if (viewModel.Arquivo1 == null || viewModel.Arquivo1.Length == 0 || !FileHelper.IsValidExtension(viewModel.Arquivo1.FileName))
            {
                ViewBag.Error = "Insira pelo menos uma imagem. Extensões aceitas: .jpg, .gif, .jpeg ou .png.";
                return await Register();
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

            using (FileStream stream = new FileStream(fullFileName + "\\1.jpg", FileMode.Create))
            {
                await viewModel.Arquivo1.CopyToAsync(stream);
            }
            using (FileStream stream = new FileStream(fullFileName + "\\2.jpg", FileMode.Create))
            {
                await viewModel.Arquivo2.CopyToAsync(stream);
            }
            using (FileStream stream = new FileStream(fullFileName + "\\3.jpg", FileMode.Create))
            {
                await viewModel.Arquivo3.CopyToAsync(stream);
            }
            return await Register();
        }

        public async Task<IActionResult> CheckIn(int id)
        {
            SingleResponse<Evento> responseEvento = await _eventoService.GetByID(id);
            int idUser = int.Parse(HttpContext.User.Claims.ToList()[2].Value);
            SingleResponse<Usuario> responseUsuario = await _usuarioService.GetByID(idUser);

            if (!responseUsuario.Success)
            {
                ViewBag.Errors = responseUsuario.Mensagem;
                return View();
            }
            if (!responseEvento.Success)
            {
                ViewBag.Errors = responseEvento.Mensagem;
                return View();
            }
            Response response = await _eventoService.CheckInUsuario(responseEvento, responseUsuario);
            if (!response.Success)
            {
                ViewBag.Errors = response.Mensagem;
                return View();
            }
            return await Index();
        }

        public async Task<IActionResult> EventoDetalhes(int? id)
        {
            if (!id.HasValue)
            {
                return await this.Index();
            }
            SingleResponse<Evento> eventoResponse = await _eventoService.GetByID(id.Value);
            if (!eventoResponse.Success)
            {
                return await this.Index();
            }



            return View();
        }
    }
}
