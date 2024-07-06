using Application.Dto;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Principal;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IBoxService, BoxService>();
            services.AddScoped<ICosmosBoxService, CosmosBoxService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>()!.HttpContext!.User);
            services.AddTransient<IValidator<CreateBoxDto>, CreateBoxDtoValidator>();

            return services;
        }
    }
}
