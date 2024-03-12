using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class TelemtryServiceConfigurator
    {
        public static IServiceCollection ConfigureTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            // The following line enables Application Insights telemetry collection.
            
            services.AddApplicationInsightsTelemetry();

            return services;
        }
    }
}