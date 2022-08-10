using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IServicoAplicacaoAuthor _repoAppAuthor;


        public AuthorController(IServicoAplicacaoAuthor repoAppAuthor)
        {
            _repoAppAuthor = repoAppAuthor;
        }

        [HttpGet("lista-de-autor")]
        public async Task<IActionResult> Index()
        {
            var authorViewmodel = await _repoAppAuthor.ObterTodos();
            return View(authorViewmodel);
        }

        [HttpGet("detalhes-de-autor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var authorViewModel = await _repoAppAuthor.ObterAuthorViewModelPorId(id);

            if (authorViewModel == null)
            {
                return NotFound();
            }

            return View(authorViewModel);
        }

        [HttpGet("adicionar-autor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("adicionar-autor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel authorViewModel)
        {
            authorViewModel.Id = Guid.NewGuid();
            if (!ModelState.IsValid) return View(authorViewModel);
            await _repoAppAuthor.AdicionarAuthor(authorViewModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar-autor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var author = await _repoAppAuthor.ObterAuthorPorId(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorViewModel = _repoAppAuthor.ObterAuthorViewModel(author);

            return View(authorViewModel);
        }


        [HttpPost("editar-autor/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
                                        AuthorViewModel authorViewModel)
        {
            if (id != authorViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repoAppAuthor.AtualizarAuthor(authorViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(authorViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(authorViewModel);
        }

        [HttpGet("excluir-autor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var authorViewModel = await _repoAppAuthor.ObterAuthorViewModelPorId(id);

            if (authorViewModel == null)
            {
                return NotFound();
            }

            return View(authorViewModel);
        }

        [HttpPost("excluir-autor/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repoAppAuthor.ExcluirAuthorPorId(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(Guid id)
        {
            return _repoAppAuthor.ObterAuthorPorId(id) != null;
        }
    }
}
