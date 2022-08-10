
using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Application.Services;
using Cooperchip.ITDeveloper.DomainCore.Notificacoes;
using Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces;
using Cooperchip.ITDeveloper.Farmacia.InfraData.Context;
using Cooperchip.ITDeveloper.Farmacia.InfraData.Repository;
using Cooperchip.ITDeveloper.Farmacia.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class BoundedContextFarmacia
    {
        public static IServiceCollection AddBoundedContextFarmacia(this IServiceCollection services, IConfiguration configuration)
        {

            // Farmacia
            services.AddScoped<IServicoAplicacaoFornecedor, ServicoAplicacaoFornecedor>();
            services.AddScoped<IServicoAplicacaoProduto, ServicoAplicacaoProduto>();
            // +
            services.AddScoped<FarmaciaDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            // Farmacia

            return services;
        }
    }


}
