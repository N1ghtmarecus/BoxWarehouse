using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBoxRepository, BoxRepository>();
            services.AddScoped<ICosmosBoxRepository, CosmosBoxRepository>();
            services.AddScoped<IPictureRepository, PictureRepository>();

            return services;
        }
    }
}
