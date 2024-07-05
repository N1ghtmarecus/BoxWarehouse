using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace WebAPI.HealthChecks
{
    public class ResponseTimeHealthCheck : IHealthCheck
    {
        private readonly Random rnd = new();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            int responseTimeMS = rnd.Next(1, 300);
            if (responseTimeMS < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy($"The response time is ideal ({responseTimeMS})."));
            }
            else if (responseTimeMS < 200)
            {
                return Task.FromResult(HealthCheckResult.Degraded($"The response time is acceptable ({responseTimeMS})."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"The response time is unacceptable ({responseTimeMS})."));
            }
        }
    }
}
