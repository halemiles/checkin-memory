using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Checkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class Telemetry
    {
        public static IWebHostBuilder UseCustomTelemetry(this IWebHostBuilder webBuilder, IConfiguration configuration)
        {
            // Add your custom logging providers or telemetry here
            webBuilder.UseSentry(o =>
            {
                o.Dsn = "";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                o.TracesSampleRate = 1.0;
            });
            return webBuilder;
        }
    }
}