using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Application.ViewModels.Farmacia;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;

namespace Cooperchip.ITDeveloper.Application.Interfaces
{
    public interface IServicoAplicacaoProduto
    {
        //========/ Leitura =====================================//
        Task<IEnumerable<ProdutoViewModel>> ObterProdutosFornecedoresAplicacao();
        Task<ProdutoViewModel> ObterProdutoApplication(Guid id);
        Task<ProdutoViewModel> PopularFornecedoresApplication(ProdutoViewModel produtoViewModel);
        //========/ Escrita =====================================//
        Task AdicionarApplication(ProdutoViewModel produtoViewModel);
        Task AtualizarApplication(ProdutoViewModel produtoViewModel);
        Task RemoverApplication(Guid id);
        //=======================================================//
    }
}
