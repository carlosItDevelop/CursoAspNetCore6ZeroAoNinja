using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cooperchip.ITDeveloper.Application.ViewModels
{
    public class AuthorViewModel
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(maximumLength:50, ErrorMessage = "O campo {0} tem de ter entre {2} e {1} caracter(es).", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Sobrenome")]
        [StringLength(maximumLength: 70, ErrorMessage = "O campo {0} tem de ter entre {2} e {1} caracter(es).", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Email")]
        [StringLength(maximumLength: 255, ErrorMessage = "O campo {0} tem de ter entre {2} e {1} caracter(es).", MinimumLength = 2)]
        public string EmailAddress { get; set; }

        [Display(Name = "Website")]
        [MaxLength(255, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string WebSite { get; set; }

        // VO
        public RedesSociais RedeSociais { get; private set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }

        public virtual IEnumerable<Telefone> Telefone { get; set; }
        public virtual IEnumerable<Tags> Tags { get; set; }
    }
}

/*
        public string Medium { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
 Github, etc.

 
 */
