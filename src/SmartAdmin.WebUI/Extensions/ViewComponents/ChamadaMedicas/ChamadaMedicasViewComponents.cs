using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Mvc.Extensions.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.ViewComponents.ChamadaMedicos
{

    [ViewComponent(Name = "ChamadaMedicas")]
    public class ChamadaMedicasViewComponents : ViewComponent
    {
        private readonly ITDeveloperDbContext _context;

        public ChamadaMedicasViewComponents(ITDeveloperDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Total = Util.TotChamadaMedico(_context);
            var notificacoes = await _context.Set<ChamadaMedica>().AsNoTracking().OrderBy(x => x.ChamadaMedicoId).Take(7).ToListAsync();
           
            return View(await Task.FromResult(notificacoes));
        }
    }
}
