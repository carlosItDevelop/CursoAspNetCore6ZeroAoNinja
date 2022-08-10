using Cooperchip.ITDeveloper.Application.AutoMapper;
using Cooperchip.ITDeveloper.Application.Extensions.Attibutes;
using Cooperchip.ITDeveloper.CrossCutting.IoC;
using Cooperchip.ITDeveloper.Mvc.Configuration;
using Cooperchip.ITDeveloper.Mvc.Data;
using Cooperchip.ITDeveloper.Mvc.Extensions.ExtensionsMethods;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services;
using Cooperchip.ITDeveloper.Mvc.Models;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cooperchip.ITDeveloper.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builer = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (env.IsProduction())
            {
                builer.AddUserSecrets<Startup>();
            }

            Configuration = builer.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContextConfig(Configuration); // In DbContextConfig
            services.AddIdentityConfig(Configuration); // In IdentityConfig
            services.AddMvcAndRazor(); // In MvcAndRazor
            services.AddDependencyInjectConfig(Configuration); // In DependencyInjectConfig
            services.AddBoundedContextFarmacia(Configuration); // BoundedContext Farmacia

            // Prover Suporte para Code Page (1252) (windows-1252)
            services.AddCodePageProviderNotSupportedInDotNetCoreForAnsi();

            services.Configure<SmartSettings>(Configuration.GetSection(SmartSettings.SectionName));

            // Note: This line is for demonstration purposes only, I would not recommend using this as a shorthand approach for accessing settings
            // While having to type '.Value' everywhere is driving me nuts (>_<), using this method means reloaded appSettings.json from disk will not work
            services.AddSingleton(s => s.GetRequiredService<IOptions<SmartSettings>>().Value);


            // Todo: Criando minha própria IOptions: StyleButtom
            services.Configure<StyleButtom>(Configuration.GetSection(StyleButtom.SectionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<StyleButtom>>().Value);


            // Todo: Criando minha própria IOptions: LeitosCapacidade
            services.Configure<LeitosESetores>(Configuration.GetSection(LeitosESetores.SectionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<LeitosESetores>>().Value);

            // Register NativeInjectorEvent : CrossCutting.IoC
            NativeInjectorEvent.AddRegisterEvent(services);

            #region: Register NativeInjectorEvent : CrossCutting.IoC
            //services.AddMediatR(typeof(Startup));

            //Domain Bus (Mediator)
            //services.AddScoped<IMediatrHandler, MediatrHandler>();
            //// Várias Subscrcrição pro mesmo Evento  - (Observer / Subscriber)
            //services.AddScoped<INotificationHandler<PacienteCadastradoEvent>, PacienteTriagemEventHandler>();
            //services.AddScoped<INotificationHandler<PacienteCadastradoEvent>, ClassificacaoPrioridadeHandler>();
            //services.AddScoped<INotificationHandler<PacienteCadastradoEvent>, PacienteSaiuAReveliaHandler>();
            //services.AddScoped<INotificationHandler<PacienteSemAvaliacaoEvent>, PacienteSemAvaliacaoHandler>();

            //services.AddScoped<INotificationHandler<AuthorCadastradoEvent>, AuthorEventHandler>();
            #endregion

            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            /* Comentei o CriaUsersAndRoles então comento aqui por warn
             * ApplicationDbContext context,
             * UserManager<ApplicationUser> userManager,
             * RoleManager<IdentityRole> roleManager
             */
            )

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();  // Obsoleto 3.1
                app.UseMigrationsEndPoint();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsProduction())
            {
                app.UseKissLogMiddleware(options =>
                {
                    options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                        Configuration["KissLog.OrganizationId"],
                        Configuration["KissLog.ApplicationId"])
                    ));
                });
            }


            var authMsgSenderOpt = new AuthMessageSenderOptions
            {
                SendGridUser = Configuration["SendGridUser"],
                SendGridKey = Configuration["SendGridKey"]
            };

            //CriaUsersAndRoles.Seed(context, userManager, roleManager).Wait();
            //app.UseMiddleware<DefaultUsersAndRolesMiddeware>();            
            //app.UseAddUserAndRoles();

            app.UseGlobalizationConfig();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    "default",
                //    "{controller=Intel}/{action=AnalyticsDashboard}");

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}");


                endpoints.MapRazorPages();
            });
        }
    }
}
