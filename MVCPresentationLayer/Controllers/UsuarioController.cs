using AutoMapper;
using Domains;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models;
using MVCPresentationLayer.Models.Usuario;
using Service.Interfaces;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ITagService _tagService;
        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, IHostingEnvironment appEnvironment, ITagService tagService)
        {
            this._usuarioService = usuarioService;
            this._mapper = mapper;
            this._appEnvironment = appEnvironment;
            this._tagService = tagService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginViewModel usuarioViewModel)
        {
            SingleResponse<Usuario> resposta = await _usuarioService.Authenticate(usuarioViewModel.Email, usuarioViewModel.Senha);
            if (resposta.Success)
            { 
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, resposta.Item.UserName),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.NameIdentifier, resposta.Item.ID.ToString()),
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Usuario");
                ClaimsPrincipal principal = new ClaimsPrincipal(new[] { identity });
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Profile", "Usuario");
            }
            ViewBag.Errors = resposta.Mensagem;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            //C:\Users\Caio Fabeni\Desktop\ConnectionParty-d333d814a75248c92f13eae19401980be2e88c8f\MVCPresentationLayer\wwwroot\imgPessoa\5.jpg
            string id = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            //string filePath = "~/imgPessoa/" + id + ".jpg";
            //Url.Content(filePath);
            string fileName = _appEnvironment.WebRootPath + FileHelper.PESSOA_DIRECTORY + id + FileHelper.EXTENSION;
            ViewBag.FileName = fileName;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            DataResponse<Tags> tags = await this._tagService.GetAll();
            ViewBag.Tags = tags.Data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioInsertViewModel viewModel)
        {
            if (viewModel.Arquivo == null || viewModel.Arquivo.Length == 0 || !FileHelper.IsValidExtension(viewModel.Arquivo.FileName))
            {
                ViewBag.Error = "Foto obrigatória. Extensões aceitas: .jpg, .gif, .jpeg ou .png.";
                return await Register();
            }

            //AutoMapper
            Usuario usuario = _mapper.Map<Usuario>(viewModel);

            string[] tags = this.Request.Form["Tags"].ToString().Split(',');

            usuario.Tags = new List<Tags>();
            foreach (var item in tags)
            {
                usuario.Tags.Add(new Tags()
                {
                    ID = int.Parse(item)
                });
            }

            Response response = await _usuarioService.Cadastrar(usuario);
            if (!response.Success)
            {
                ViewBag.Error = response.Mensagem;
                return await Register();
            }

            //< obtém o caminho físico da pasta wwwroot >
            string caminho_WebRoot = _appEnvironment.WebRootPath;
            string id = usuario.ID.ToString();
            string fullFileName = caminho_WebRoot + FileHelper.PESSOA_DIRECTORY + id + FileHelper.EXTENSION;

            using (FileStream stream = new FileStream(fullFileName, FileMode.Create))
            {
                await viewModel.Arquivo.CopyToAsync(stream);
            }

            return await Profile();
        }
    }
}
