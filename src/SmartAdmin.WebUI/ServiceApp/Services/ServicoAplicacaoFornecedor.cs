using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Application.ViewModels.Farmacia;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;

namespace Cooperchip.ITDeveloper.Application.Services
{
    public class ServicoAplicacaoFornecedor : IServicoAplicacaoFornecedor
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public ServicoAplicacaoFornecedor(IFornecedorRepository fornecedorRepository,
                                           IFornecedorService fornecedorService,
                                           IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        // ========/ Leitura =========================================//
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodosApplication()
        {
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }
        public async Task<FornecedorViewModel> ObterFornecedorEnderecoApplication(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
        public async Task<FornecedorViewModel> ObterFornecedorProdutosEnderecoApplication(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

        // ========/ Escrita =========================================//

        public async Task AdicionarApplication(FornecedorViewModel fvm)
        {
            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fvm));
        }
        public async Task AtualizarApplication(FornecedorViewModel fvm)
        {
            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fvm));
        }
        public async Task RemoverApplication(Guid id)
        {
            await _fornecedorService.Remover(id);
        }
        public async Task AtualizarEnderecoApplication(FornecedorViewModel fvm)
        {
            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fvm.Endereco));
        }

    }
}
