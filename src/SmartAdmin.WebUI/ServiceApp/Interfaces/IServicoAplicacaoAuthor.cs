using Cooperchip.ITDeveloper.Application.ViewModels;
using Cooperchip.ITDeveloper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Application.Interfaces
{
    public interface IServicoAplicacaoAuthor
    {

        //Task<IEnumerable<AuthorViewModel>> ObterListaAuthorViewModel();
        Task<Author> ObterAuthorPorId(Guid id);
        Task<AuthorViewModel> ObterAuthorViewModelPorId(Guid id);
        AuthorViewModel ObterAuthorViewModel(Author author);
        Task<IEnumerable<AuthorViewModel>> ObterTodos();


        Task AdicionarAuthor(AuthorViewModel authorViewModel);
        Task AtualizarAuthor(AuthorViewModel authorViewModel);
        Task ExcluirAuthorPorId(Guid id);

    }
}
