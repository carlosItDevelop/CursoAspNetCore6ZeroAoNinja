using AutoMapper;
using Cooperchip.ITDeveloper.Application.ViewModels;
using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITDeveloperDbContext _context;
        private readonly IMapper _mapper;

        public TagsController(ITDeveloperDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("lista-de-tag")]
        public async Task<IActionResult> Index()
        {
            var tags = await _context.Tags.Include(x=>x.Author).AsNoTracking().ToListAsync();
            return View(_mapper.Map<IEnumerable<TagsViewModel>>(tags));
        }

        [HttpGet("detalhes-de-tags/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var tagsViewModel = _mapper.Map<TagsViewModel>(await _context.Tags
                .Include(t => t.Author)
                .FirstOrDefaultAsync(m => m.Id == id));
            if (tagsViewModel == null)
            {
                return NotFound();
            }

            return View(tagsViewModel);
        }

        [HttpGet("adicionar-tag")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name");
            return View();
        }

        [HttpPost("adicionar-tag")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagsViewModel tagsViewModel)
        {
            if (!ModelState.IsValid) return View(tagsViewModel);

            var tag = _mapper.Map<Tags>(tagsViewModel);
            // If Operação Válida
            tag.Id = Guid.NewGuid();
            _context.Set<Tags>().Add(tag);
            await _context.SaveChangesAsync();

            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", tag.AuthorId);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("editar-tag/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var tags = _mapper.Map<TagsViewModel>(await _context.Tags.FindAsync(id));
            //var autor = await _context.Author.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tags.AuthorId);

            if (tags == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", tags.AuthorId);
            return View(tags);
        }

        [HttpPost("editar-tag/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TagsViewModel tagsViewModel)
        {
            if (id != tagsViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Set<Tags>().Update(_mapper.Map<Tags>(tagsViewModel));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagsExists(tagsViewModel.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", tagsViewModel.AuthorId);
            return View(tagsViewModel);
        }

        [HttpGet("excluir-tag/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tagsViewModel = _mapper.Map<TagsViewModel>(await _context.Tags
                .Include(t => t.Author)
                .FirstOrDefaultAsync(m => m.Id == id));
            if (tagsViewModel == null)
            {
                return NotFound();
            }

            return View(tagsViewModel);
        }
        
        [HttpPost("excluir-tag/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {


            var tags = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tags);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagsExists(Guid id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
