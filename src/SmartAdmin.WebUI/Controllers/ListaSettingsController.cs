using System.Collections.Generic;
using Cooperchip.ITDeveloper.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class ListaSettingsController : Controller
    {
        private readonly LeitosESetores _settings;

        public ListaSettingsController(IOptions<LeitosESetores> settings)
        {
            _settings = settings.Value;
        }

        public IActionResult Index()
        {
            var lista = new List<string>();
            lista.Add($"Leitos Capacidade:  {_settings.Leito.Capacidade}");
            lista.Add($"Leitos Ocupados:  {_settings.Leito.Ocupados}");
            lista.Add($"Leitos Disponíveis:  {_settings.Leito.Disponiveis}");
            lista.Add($"Leitos em Manutenção:  {_settings.Leito.Emmanutencao}");
            lista.Add($"Leitos Fora de Serviço:  {_settings.Leito.Foradeservico}");

            lista.Add($"Nº de Setor-CTI:  {_settings.Setor.Cti}");
            lista.Add($"Nº de Setor-UTI:  {_settings.Setor.Uti}");
            lista.Add($"Nº de Setor-Quarto:  {_settings.Setor.Quarto}");
            lista.Add($"Nº de Setor-Emergência:  {_settings.Setor.Emergencia}");
            lista.Add($"Nº de Setor-Enfermaria:  {_settings.Setor.Enfermaria}");


            return View(lista);
        }

    }
}
