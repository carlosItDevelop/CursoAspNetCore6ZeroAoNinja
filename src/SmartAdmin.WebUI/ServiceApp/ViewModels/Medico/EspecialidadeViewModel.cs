using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cooperchip.ITDeveloper.Application.ViewModels.Medico
{
    public class EspecialidadeViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Descrição")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public virtual ICollection<MedicoViewModel> Medico { get; set; }
    }
}
