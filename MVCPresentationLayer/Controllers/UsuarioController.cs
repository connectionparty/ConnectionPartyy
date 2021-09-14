using AutoMapper;
using Domains;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models;
using MVCPresentationLayer.Models.Usuario;
using Service.Interfaces;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioService _usuarioService;
        private IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            this._usuarioService = usuarioService;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
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
                    new Claim(ClaimTypes.Name, resposta.Item.Email),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.NameIdentifier, resposta.Item.ID.ToString()),
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Usuario");
                ClaimsPrincipal principal = new ClaimsPrincipal(new[] { identity });
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Errors = resposta.Mensagem;
            return View();
        }


        public IActionResult Teste()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UsuarioInsertViewModel viewModel)
        {
            //AutoMapper
            Usuario usuario = _mapper.Map<Usuario>(viewModel);

            Response response = await _usuarioService.Cadastrar(usuario);
            if (response.Success)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = response.Mensagem;

            return View();
        }
    }
}
