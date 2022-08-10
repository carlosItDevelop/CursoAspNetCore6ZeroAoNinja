﻿using Cooperchip.ITDeveloper.Application.Interfaces;
using Cooperchip.ITDeveloper.Application.Services;
using Cooperchip.ITDeveloper.CrossCutting.Auxiliar;
using Cooperchip.ITDeveloper.CrossCutting.Helpers;
using Cooperchip.ITDeveloper.Data.Repository;
using Cooperchip.ITDeveloper.Data.UoW;
using Cooperchip.ITDeveloper.Domain.Interfaces;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using Cooperchip.ITDeveloper.Domain.Services;
using Cooperchip.ITDeveloper.DomainCore.Notificacoes;
using Cooperchip.ITDeveloper.Mvc.Extensions.Filters;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services;
using Cooperchip.ITDeveloper.Mvc.Intra;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection AddDependencyInjectConfig(this IServiceCollection services, IConfiguration configuration)
        {

            // Application
            services.AddScoped<IServicoAplicacaoPaciente, ServicoAplicacaoPaciente>();
            services.AddScoped<IServicoAplicacaoAuthor, ServicoAplicacaoAutor>();
            services.AddScoped<IAppTriagem, ServiceAppTriagem>();


            // Domain => Service
            services.AddScoped<IAuthorService, ServiceDomainAuthor>();
            services.AddScoped<IPacienteService, ServiceDomainPaciente>();
            services.AddScoped<ITriagemService, ServiceDomainTriagem>();
            services.AddScoped<ICidService, ServiceDomainCid>();


            // Domain => Repository
            services.AddScoped<IRepositoryPaciente, PacienteRepository>();
            services.AddScoped<IRepositoryAuthor, AuthorRepository>();
            services.AddScoped<IRepositoryTriagem, TriagemRepositorio>();
            services.AddScoped<IRepositoryCid, RepositoryCid>();


            services.AddScoped<IPacienteTesteRepository, PacienteTesteRepository>();


            // Notificações
            services.AddScoped<INotificador, Notificador>();


            // The Unit of Work register need to be Scopped LC;
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUnitOfUpload, UnitOfUpload>();

            // =====/ Mantem o estado do contexto Http por toda a aplicação === //
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ========================================================== //
            services.AddScoped<IUserInContext, AspNetUser>();
            services.AddScoped<IUserInAllLayer, UserInAllLayer>();
            // ========================================================== //

            // =====/ Adicionar Claims para HttpContext >> toda a Applications ================ //
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsService>();
            // ========================================================== //

            services.AddScoped((context) => Logger.Factory.Get());
            services.AddScoped<AuditoriaIloggerFilter>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(configuration);

            return services;
        }
    }
}