﻿using AutoMapper;
using Domains;
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

        public EventoController(IEventoService eventoService, IUsuarioService usuarioService, IMapper mapper, IHostingEnvironment appEnvironment)
        {
            this._eventoService = eventoService;
            this._usuarioService = usuarioService;
            this._mapper = mapper;
            this._appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(EventoInsertViewModel viewModel)
        {
            Evento evento = _mapper.Map<Evento>(viewModel);

            int id = int.Parse(HttpContext.User.Claims.ToList()[2].Value);
            SingleResponse<Usuario> responseUsuario = await _usuarioService.GetByID(id);

            if (responseUsuario.Success)
            {
                evento.UsuarioID = responseUsuario.Item.ID;
            }

            if (viewModel.Arquivo == null || viewModel.Arquivo.Length == 0 || !FileHelper.IsValidExtension(viewModel.Arquivo.FileName))
            {
                ViewBag.Error = "Insira pelo menos uma imagem. Extensões aceitas: .jpg, .gif, .jpeg ou .png.";
                return View();
            }

            Response response = await _eventoService.Cadastrar(evento);
            if (!response.Success)
            {
                ViewBag.Error = response.Mensagem;
                return View();
            }

            //< obtém o caminho físico da pasta wwwroot >
            string caminho_WebRoot = _appEnvironment.WebRootPath;
            string idEvento = evento.ID.ToString();
            string fullFileName = caminho_WebRoot + FileHelper.EVENTO_DIRECTORY + idEvento;

            Directory.CreateDirectory(fullFileName);

            using (FileStream stream = new FileStream(fullFileName + "\\1.jpg", FileMode.Create))
            {
                await viewModel.Arquivo.CopyToAsync(stream);
            }
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            return View();
        }
    }
}
