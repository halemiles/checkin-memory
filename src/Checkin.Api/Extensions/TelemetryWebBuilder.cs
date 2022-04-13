using Microsoft.AspNetCore.Hosting;

namespace Checkin.Api.Extensions
{
    public static class Telemetry
    {    
        public static IWebHostBuilder useCustomTelemetry(this IWebHostBuilder webBuilder)
        {           
            // Add your custom logging providers or telemetry here
            return webBuilder;
        }
    }
}