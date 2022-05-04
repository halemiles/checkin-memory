using Microsoft.AspNetCore.Hosting;

namespace Checkin.Api.Extensions
{
    public static class Telemetry
    {
        public static IWebHostBuilder UseCustomTelemetry(this IWebHostBuilder webBuilder)
        {
            return webBuilder;
        }
    }
}