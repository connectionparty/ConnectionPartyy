using AutoMapper;
using Domains;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models;
using MVCPresentationLayer.Models.Usuario;
using Service.Interfaces;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //DataResponse<Usuario> response = await this._usuarioService.LerGeneros();
            //if (!response.Success)
            //{
            //    ViewBag.Errors = response.Mensagem;
            //}
            //List<UsuarioQueryViewModel> generos =
            //_mapper.Map<List<UsuarioQueryViewModel>>(response.Data);

            //return View(generos);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Teste()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UsuarioInsertViewModel viewModel)
        {
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
