using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Application.ViewModels;
using Cooperchip.ITDeveloper.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PacienteController : Controller
    {

        private readonly IServicoAplicacaoPaciente _servicoAplicacaoPaciente;
        private readonly SmartSettings _appSettings;

        public PacienteController(IServicoAplicacaoPaciente servicoAplicacaoPaciente,
                                  IOptions<SmartSettings> appSettings)
        {
            this._servicoAplicacaoPaciente = servicoAplicacaoPaciente;
            this._appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet("lista-de-paciente")]
        public async Task<IActionResult> Index()
        {
            return View(await _servicoAplicacaoPaciente.PacienteParaPacienteViewModel());
        }


        [HttpGet("detalhe-de-paciente/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var paciente = await this._servicoAplicacaoPaciente.ObterPacienteComEstadoPaciente(id);

            if (paciente == null)
            {
                return NotFound();
            }

            /// Todo; Depois apagar.
            /// Apenas teste de IOptions com DI na Controller;
            List<string> opt = new List<string>();
            opt.Add(_appSettings.App);
            opt.Add(_appSettings.AppFlavor);
            opt.Add(_appSettings.AppFlavorSubscript);
            opt.Add(_appSettings.AppName);
            opt.Add(_appSettings.Theme.ThemeVersion);
            var testeOpt = opt;

            return View(paciente);
        }

        [HttpGet("relatorio-por-estado-de-paciente/{id:guid}")]
        public async Task<IActionResult> ReportForEstadoPaciente(Guid id)
        {
            var pacientePorEstado
                = await this._servicoAplicacaoPaciente.ObterPacientesPorEstadoPaciente(id);

            if (pacientePorEstado == null) return NotFound();

            return View(pacientePorEstado);
        }


        [HttpGet("novo-paciente")]
        public IActionResult Create()
        {
            // Todo: Popular EstadoPaciente para DropDown e acabar com esta ViewBag;

            ViewBag.EstadoPaciente =
                new SelectList(_servicoAplicacaoPaciente.ListaEstadoPacienteApplication(), "Id", "Descricao");

            return View();
        }


        [HttpPost("novo-paciente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteViewModel pacienteViewModel)
        {
            if (ModelState.IsValid)
            {
                await this._servicoAplicacaoPaciente.AdicionarPacienteApplication(pacienteViewModel);
                return RedirectToAction("Index");
            }

            ViewBag.EstadoPaciente =
                new SelectList(_servicoAplicacaoPaciente.ListaEstadoPacienteApplication(), "Id", "Descricao", pacienteViewModel.EstadoPacienteId);

            return View(pacienteViewModel);
        }

        [HttpGet("editar-paciente/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var pacienteViewModel = await _servicoAplicacaoPaciente.ObterPacientePorIdApplication(id);
            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            ViewBag.EstadoPaciente =
                new SelectList(_servicoAplicacaoPaciente.ListaEstadoPacienteApplication(), "Id", "Descricao", pacienteViewModel.EstadoPacienteId);


            return View(pacienteViewModel);
        }

        [HttpPost("editar-paciente/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PacienteViewModel pacienteViewModel)
        {
            if (id != pacienteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this._servicoAplicacaoPaciente.EditarPacienteApplication(pacienteViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(pacienteViewModel.Id))
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

            ViewBag.EstadoPaciente =
                new SelectList(_servicoAplicacaoPaciente.ListaEstadoPacienteApplication(), "Id", "Descricao", pacienteViewModel.EstadoPacienteId);

            return View(pacienteViewModel);
        }

        [HttpGet("excluir-paciente/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var paciente = await _servicoAplicacaoPaciente.ObterPacienteComEstadoPaciente(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpPost("excluir-paciente/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _servicoAplicacaoPaciente.RemoverPacienteApplication(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("historico-de-paciente")]
        public async Task<IActionResult> Historico()
        {
            return View();
        }


        [HttpGet("GetHistoricoPaciente")]
        public async Task<JsonResult> GetHistoricoPaciente()
        {
            var pacientes = await _servicoAplicacaoPaciente.ObterPacientesTeste();
            dynamic response = new
            {
                data = pacientes
            };

            return Json(response);
        }

        private void PopularEstadoPaciente(Guid pacienteId)
        {
            // Todo: Código aqui
        }

        private bool PacienteExists(Guid id)
        {
            return _servicoAplicacaoPaciente.TemPaciente(id);
        }
    }
}
