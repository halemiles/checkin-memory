using Checkin.Api.Models;
using Checkin.Repositories;
using Checkin.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Checkin.Api.Extensions
{
    public static class TelemtryServiceConfigurator
    {
        public static IServiceCollection ConfigureTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var memoryProviderSettings = configuration.GetSection("MemoryProvider").Get<MemoryProviderSettings>();

            // if(memoryProviderSettings.Name == "DistributedCache")
            // {
            //     Log.Information("Using distributed cache");
            //     services.AddDistributedRedisCache(option =>
            // {
            //     option.Configuration = "127.0.0.1";
            //     option.InstanceName = "master";
            // });
            //     services.AddScoped<IDeviceCacheRepository, DistributedDeviceCacheRepository>();
            // }
            // else 
            // {
            //     Log.Information("Using IMemoryCache");
            //     services.AddMemoryCache();
            //     services.AddScoped<IDeviceCacheRepository, DeviceCacheRepository>();
            // }

            return services;
        }
    }
}