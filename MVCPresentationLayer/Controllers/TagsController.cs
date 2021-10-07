using AutoMapper;
using Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models;
using Service.Interfaces;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;

        public TagsController(ITagService tagService, IUsuarioService usuarioService, IMapper mapper, IHostingEnvironment appEnvironment)
        {
            this._tagService = tagService;
            this._usuarioService = usuarioService;
            this._mapper = mapper;
            this._appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(TagInstertViewModel viewModel)
        {
            Tags tag = _mapper.Map<Tags>(viewModel);

            Response tagsResponse = await _tagService.Cadastrar(tag);
            if (!tagsResponse.Success)
            {
                ViewBag.Error = tagsResponse.Mensagem;
                return View();
            }

            return View();

        }
    }
}
