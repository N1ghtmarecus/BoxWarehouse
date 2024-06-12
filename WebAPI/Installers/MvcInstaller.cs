using Application;
using Application.Services;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebAPI.Middlewares;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfrastructure();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddAuthorization();

            services.AddTransient<UserResolverService>();
            services.AddScoped<ErrorHandlerMiddleware>();
        }
    }
}
