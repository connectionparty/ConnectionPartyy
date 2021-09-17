using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class EventoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult Register()
        //{

        //    return View();
        //}
    }
}
