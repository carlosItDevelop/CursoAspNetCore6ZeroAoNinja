using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Cooperchip.ITDeveloper.Mvc.Models
{
    public class LeitosESetores
    {
        public const string SectionName = nameof(LeitosESetores);

        public Leito Leito { get; set; }
        public Setor Setor { get; set; }
    }

    public class Leito
    {
        public string Capacidade { get; set; }
        public string Ocupados { get; set; }
        public string Disponiveis { get; set; }
        public string Emmanutencao { get; set; }
        public string Foradeservico { get; set; }
    }

    public class Setor
    {
        public string Cti { get; set; }
        public string Uti { get; set; }
        public string Quarto { get; set; }
        public string Emergencia { get; set; }
        public string Enfermaria { get; set; }
    }

}


