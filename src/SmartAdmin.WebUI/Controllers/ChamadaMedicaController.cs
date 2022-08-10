using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class ChamadaMedicaController : Controller
    {
        private readonly ITDeveloperDbContext _context;

        public ChamadaMedicaController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        //[HttpGet("chamada-medico")]
        public async Task<IActionResult> Index()
        {
            var model = await _context.ChamadaMedica.OrderBy(x=>x.DataChamada).AsNoTracking().ToListAsync();

            return View(model);
        }

        //[HttpGet("chamada-medico-detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) NotFound("Id não pode ser Nulo.");

            var model = await _context.ChamadaMedica.AsNoTracking().FirstOrDefaultAsync(x => x.ChamadaMedicoId == id);

            if (model == null) return NotFound("Modelo não excontrado!");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditModal(int id)
        {
            var model = await _context.ChamadaMedica.AsNoTracking().FirstOrDefaultAsync(x => x.ChamadaMedicoId == id);

            if (model == null) return NotFound("Modelo não excontrado!");

            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChamadaMedica chamada)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(chamada);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chamada);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cm = await _context.ChamadaMedica
                .FirstOrDefaultAsync(m => m.ChamadaMedicoId == id);
            if (cm == null)
            {
                return NotFound();
            }

            return View(cm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cm = await _context.ChamadaMedica.FindAsync(id);
            _context.ChamadaMedica.Remove(cm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
