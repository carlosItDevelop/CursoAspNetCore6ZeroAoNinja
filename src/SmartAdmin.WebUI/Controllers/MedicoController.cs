using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IServicoAplicacaoPaciente _teste;

        public MedicoController(IServicoAplicacaoPaciente teste)
        {
            _teste = teste;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("GetPacienteTeste")]
        public async Task<JsonResult> GetPacienteTeste()
        {
            var pacientes = await _teste.ObterPacientesTeste();
            dynamic response = new
            {
                data = pacientes
            };

            return Json(response);
        }


    }
}
