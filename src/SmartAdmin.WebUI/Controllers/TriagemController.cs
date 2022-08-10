using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class TriagemController : Controller
    {
        private readonly IAppTriagem _appTriagem;

        public TriagemController(IAppTriagem appTriagem)
        {
            _appTriagem = appTriagem;
        }

        //[HttpGet("listagem-notificacoes")]
        public async Task<IActionResult> Index()
        {
            var lista = await _appTriagem.ListaTriagemPorData();
            return View(lista);
        }

        //[HttpGet("triagem-para-prontuario/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var triagem = await _appTriagem.ObterTriagemPorId(id);

            if (triagem == null) return BadRequest();

            return View(triagem);
        }


        //[HttpGet("nova-triagem")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //[HttpPost("adicionando-triagem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Triagem triagem)
        {
            if (ModelState.IsValid)
            {                
                await this._appTriagem.IncluirTriagem(triagem);
                return RedirectToAction("Index");
            }

            return View(triagem);
        }


        public async Task<IActionResult> Delete(Guid id)
        {


            var triagem = await _appTriagem.ObterPorId(id);

            if (triagem == null)
            {
                return NotFound();
            }

            return View(triagem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _appTriagem.ExcluirTriagemPorId(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
