using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Cooperchip.GestaoHospitalar.Identidade.API.Extensions
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddAddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Medical Management Sys - API",
                    Description = "Esta API serve recursos do Sistema Gestão Hospitalar Medical Management v.1.0.0",
                    Contact = new OpenApiContact()
                    {
                        Name = "Carlos A Santos",
                        Email = "carlos.itdeveloper@gmail.com",
                        Url = new Uri("https://cooperchip.com.br")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                })
            );
            return services;
        }



        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }

    }
}
