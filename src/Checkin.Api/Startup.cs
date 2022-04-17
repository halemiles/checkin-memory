using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Checkin.Models;
using Checkin.Services;
using Checkin.Services.Interfaces;
using Checkin.Repositories;
using Checkin.Api.Models;
using Serilog;

namespace Checkin.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting API");
            
            services.AddAutoMapper(mapperConfig => {
                mapperConfig.AddProfile<DeviceDtoToDeviceProfile>();
                mapperConfig.AddProfile<DeviceToDeviceMergeProfile>();
                mapperConfig.AddProfile<DeviceNetworkToDeviceNetworkDtoProfile>();
            });
            var memoryProviderSettings = Configuration.GetSection("MemoryProvider").Get<MemoryProviderSettings>();
            
            if(memoryProviderSettings.Name == "DistributedCache")
            {
                Log.Information("Using distributed cache");
                services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";
                option.InstanceName = "master";
            });
                services.AddScoped<IDeviceCacheRepository, DistributedDeviceCacheRepository>();
            }
            else
            {
                Log.Information("Using IMemoryCache");
                services.AddMemoryCache();
                services.AddScoped<IDeviceCacheRepository, DeviceCacheRepository>();
            }

            services.AddScoped<IDeviceService, DeviceService>();

            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Checkin.Api", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkin.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
