using Cooperchip.ITDeveloper.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cooperchip.ITDeveloper.Application.ViewModels
{
    public class TagsViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(90, ErrorMessage = "O campo {0} precisa ter entre {2} e (1) caracter(es).", MinimumLength = 2)]
        [Display(Name = "Tags")]
        public string Tag { get; set; }


        /// <summary>
        /// Descrição diversa sobre a anotação, como link, Título, etc.
        /// </summary>
        [Display(Name = "Anotações")]
        public string Note { get; set; }

        /// <summary>
        /// Campo reservado para dizer onde 
        /// está ou de onde veio, etc
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Origem")]
        public string SourceAt { get; set; }

        public virtual Author Author { get; set; }

        [Display(Name = "Autor")]
        public Guid AuthorId { get; set; }
    }
}
