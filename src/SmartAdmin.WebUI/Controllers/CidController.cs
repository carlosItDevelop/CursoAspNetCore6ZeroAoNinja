using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cooperchip.ITDeveloper.Application.Extensions;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Application.ViewModels;
using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.Entities.Utils;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    //[Route("[Controller]")]
    public class CidController : Controller
    {


        // (Query) - Get ==> Fala dreto com a camada de Dados
        private readonly IRepositoryCid _repoCid;

        // Cuida do Mapeamento Model/ViewModel + Reverse,
        // antes de passar para Repositório ou Seviços de Domain
        private readonly IMapper _mapper;

        private readonly ITDeveloperDbContext _context;

        public CidController(ITDeveloperDbContext context,
                             IRepositoryCid repoCid)
        {
            _context = context;
            _repoCid = repoCid;
        }

        [HttpGet("lista-cids-paginada")]
        public async Task<IActionResult> ListaCidsPaginada([FromQuery] int ps = 8,
                                                           [FromQuery] int page = 1,
                                                           [FromQuery] string q = null)
        {
            var cids = await _repoCid.ObterTodasPaginada(ps, page, q = null);
            //ViewBag.Pesquisa = q;
            ViewData["pesquisa"] = q;
            cids.ReferenceAction = "lista-paginada";

            return View(model: cids);
        }

        [HttpGet("cids-paginada")]
        public IActionResult CidsPaginada([FromQuery] QueryParameters queryParameters)
        {
            var cids = _repoCid.ObterCidsPaginada(queryParameters);
            ViewData["pesquisa"] = "";
            //cids.ReferenceAction = "cids-paginada";

            return View(cids);
        }

        //ObterCidsPaginada

        [HttpGet("lista-de-cid")]
        public async Task<IActionResult> Index(int? pagina, string ordenacao, string stringBusca)
        {
            //var usuario = HttpContext.User.Identity.Name;

            const int itensPorPagina = 8;
            int numeroPagina = (pagina ?? 1);

            ViewData["ordenacao"] = ordenacao;
            ViewData["filtroAtual"] = stringBusca;

            var cids = from c in _context.Cid select c;

            if (!string.IsNullOrEmpty(stringBusca)) cids = cids.Where(s => s.Codigo.Contains(stringBusca) || s.Diagnostico.Contains(stringBusca));

            ViewData["OrderByInternalId"] = string.IsNullOrEmpty(ordenacao) ? "CidInternalId_desc" : "";
            ViewData["OrderByCodigo"] = ordenacao == "Codigo" ? "Codigo_desc" : "Codigo";
            ViewData["OrderByDiagnostico"] = ordenacao == "Diagnostico" ? "Diagnostico_desc" : "Diagnostico";

            if (string.IsNullOrEmpty(ordenacao)) ordenacao = "CidInternalId";

            if (ordenacao.EndsWith("_desc"))
            {
                ordenacao = ordenacao.Substring(0, ordenacao.Length - 5);
                cids = cids.OrderByDescending(x => EF.Property<object>(x, ordenacao));
            }
            else
            {
                cids = cids.OrderBy(x => EF.Property<object>(x, ordenacao));
            }

            return View(cids.AsNoTracking().ToListAsync());

            //return View(await cids.AsNoTracking().ToListAsync(numeroPagina, itensPorPagina));
        }

        [HttpGet("arquivo-invalido")]
        public IActionResult ArquivoInvalido()
        {
            TempData["ArquivoInvalido"] = "O Arquivo não é válido!";
            return View();
        }


        [HttpPost("importa-cid")]
        public async Task<IActionResult> ImportCid(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            // DRY
            if (!ArquivoValido.EhValido(file, "Cid.Csv")) return RedirectToAction("ArquivoInvalido"); // DELEGUEI

            var filePah = $"{webHostEnvironment.WebRootPath}\\importFiles\\{file.FileName}";

            CopiarArquivo.Copiar(file, filePah); // Deleguei

            int k = 0;
            string line;

            List<Cid> cids = new List<Cid>();
            Encoding encodingPage = Encoding.GetEncoding(1252);
            bool detectEncoding = false;

            using (var fs = System.IO.File.OpenRead(filePah))
            using (var stream = new StreamReader(fs, encoding: encodingPage, detectEncoding))
                while ((line = stream.ReadLine()) != null)
                {
                    string[] parts = line.Split(";");
                    // cidinternalid, codigo, diagnostico  (os campos que vem no cabecalho do .csv)
                    if (k > 0) // Pular Cabechalho
                    {
                        if (!_context.Cid.Any(e => e.CidInternalId == int.Parse(parts[0])))
                        {
                            cids.Add(new Cid
                            {
                                CidInternalId = int.Parse(parts[0]),
                                Codigo = parts[1],
                                Diagnostico = parts[2]
                            });
                        }
                    }
                    k++;
                }

            if (cids.Any())
            {
                await _context.AddRangeAsync(cids);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // CRUD Aqui
        [HttpGet("detalhes-cid")]
        public async Task<IActionResult> Details(Guid id)
        {

            var cid = await _context.Cid.FirstOrDefaultAsync(m => m.Id == id);
            if (cid == null) return NotFound();

            return View(cid);
        }

        [HttpGet("nova-cid")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("adicionar-cid")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cid cid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cid);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cid);
        }

        [HttpGet("view-cid")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cid = await _context.Cid.FindAsync(id);
            if (cid == null) return NotFound();

            return View(cid);
        }

        [HttpPost("editar-cid")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Cid cid)
        {
            if (id != cid.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidExists(cid.Id))
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
            return View(cid);
        }

        [HttpGet("view-delete-cid")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cid = await _context.Cid.FirstOrDefaultAsync(m => m.Id == id);
            if (cid == null) NotFound();

            return View(cid);
        }

        [HttpPost, ActionName("Delete")]
        [Route("excluir-cid")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cid = await _context.Cid.FindAsync(id);
            _context.Cid.Remove(cid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CidExists(Guid id)
        {
            return _context.Cid.Any(x => x.Id == id);
        }
    }
}
