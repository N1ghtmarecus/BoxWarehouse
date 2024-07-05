using Infrastructure.Data;
using WebAPI.HealthChecks;

namespace WebAPI.Installers
{
    public class HealthChecskInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<BoxWarehouseContext>("Database");

            services.AddHealthChecks()
                .AddCheck<ResponseTimeHealthCheck>("Network speed test");

            services.AddHealthChecksUI()
                .AddInMemoryStorage();
        }
    }
}
