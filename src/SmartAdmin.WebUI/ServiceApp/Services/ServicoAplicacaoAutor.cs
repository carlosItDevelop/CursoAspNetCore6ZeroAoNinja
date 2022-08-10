using AutoMapper;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Application.ViewModels;
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.Interfaces;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Application.Services
{
    public class ServicoAplicacaoAutor : IServicoAplicacaoAuthor
    {
        // (Query) - Get ==> Fala dreto com a camada de Dados
        private readonly IRepositoryAuthor _repo;

        // (Command) - Post, Put, Patch, Delete - Fala com Domain
        private readonly IAuthorService _authorService;

        // Cuida do Mapeamento Model/ViewModel + Reverse,
        // antes de passar para Repositório ou Seviços de Domain
        private readonly IMapper _mapper;

        public ServicoAplicacaoAutor(IRepositoryAuthor repo,
                                     IAuthorService authorService,
                                     IMapper mapper)
        {
            _repo = repo;
            _authorService = authorService;
            _mapper = mapper;
        }

        public async Task<Author> ObterAuthorPorId(Guid id)
        {
            return await _repo.SelecionarPorId(id);
        }
        public AuthorViewModel ObterAuthorViewModel(Author author)
        {
            var authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return authorViewModel;
        }
        public async Task<IEnumerable<AuthorViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<AuthorViewModel>>(await _repo.SelecionarTodos());            
        }
        public async Task<AuthorViewModel> ObterAuthorViewModelPorId(Guid id)
        {
            return _mapper.Map<AuthorViewModel>(await _repo.SelecionarPorId(id));
        }

        // =======================================================================


        // =======================================================================
        /* ===/ Estes três métodos abaixo delegam a responsabilidade para os Domain Srvices, 
         * pois lá haverá as validações de Regra de Negócios e Validações 
         */

        public async Task AdicionarAuthor(AuthorViewModel authorViewModel)
        {
            var author = _mapper.Map<Author>(authorViewModel);
            await _authorService.AdicionarAuthor(author);
        }
        public async Task AtualizarAuthor(AuthorViewModel authorViewModel)
        {
            var author = _mapper.Map<Author>(authorViewModel);
            await _authorService.AtualizarAutor(author);
        }
        public async Task ExcluirAuthorPorId(Guid id)
        {
            var author = await _repo.SelecionarPorId(id);
            await _authorService.ExcluirAuthor(author);
        }
    }
}
