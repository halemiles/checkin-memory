using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkin.Api.Extensions
{
    public static class TelemtryServiceConfigurator
    {
        public static IServiceCollection ConfigureTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            // The following line enables Application Insights telemetry collection.
            var appInsightsKey = configuration.GetSection("ApplicationInsights").Get<ApplicationInsightsSettings>();
            if(!string.IsNullOrEmpty(appInsightsKey.ConnectionString))
            {
                services.AddApplicationInsightsTelemetry();
            }

            return services;
        }
    }
}