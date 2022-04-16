using Microsoft.AspNetCore.Hosting;

namespace Checkin.Api.Extensions
{
    public static class Telemetry
    {
        public static IWebHostBuilder UseCustomTelemetry(this IWebHostBuilder webBuilder)
        {
            // Add your custom logging providers or telemetry here
            webBuilder.UseSentry(o =>
            {
                o.Dsn = "https://a4a4924209644cdab202f220c8d2486d@o332883.ingest.sentry.io/6298913";
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