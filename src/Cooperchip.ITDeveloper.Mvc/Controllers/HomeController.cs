using Microsoft.AspNetCore.Mvc;
using System;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{

    [Route("")]
    [Route("gestao-de-paciente")]
    [Route("gestao-de-pacientes")]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        [Route("")]
        [Route("pagina-inicial")]
        public IActionResult Index()
        {
              return View();
        }


        //[Authorize(Roles = "Admin")]
        [Route("dashboard")]
        [Route("pagina-de-estatistica")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Route("box-init")]
        public IActionResult BoxInit()
        {
            return View();
        }

        [Route("quem-somos")]
        [Route("sobre-nos")]
        [Route("sobre/{id:guid}/{paciente}/{categoria?}")]
        public IActionResult Sobre(Guid id, string paciente, string categoria)
        {
            return View();
        }


        [Route("privacidade")]
        [Route("politica-de-privacidade")]
        public IActionResult Privacy()
        {
            return View();
        }


    }
}
