using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Checkin.Models;
using Checkin.Services;
using Checkin.Services.Interfaces;
using Checkin.Repositories;
using Checkin.Api.Models;
using Serilog;
using Serilog.Exceptions;
using StackExchange.Redis;
using Checkin.Api.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Api
{
    [ExcludeFromCodeCoverage]
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
            services.ConfigureTelemetry(Configuration);

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder
                        .WithOrigins("http://localhost:3000")
                        .WithOrigins("http://localhost:5000")
                        .WithOrigins("https://localhost:5001")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // We only want to add Seq if we have defined a host
            var seqSettings = Configuration.GetSection("Seq").Get<SeqSettings>();
            if(seqSettings.UseSeq)
            {
                logger.WriteTo.Seq(seqSettings.Host,
                 apiKey: seqSettings.ApiKey);
            }

            Log.Logger = logger.CreateLogger();

            Log.Information("Configuring services");
            services.AddSingleton(Log.Logger);

            //TODO - Move to extension method
            services.AddAutoMapper(mapperConfig => {
                mapperConfig.AddProfile<DeviceDtoToDeviceProfile>();
                mapperConfig.AddProfile<DeviceToDeviceMergeProfile>();
                mapperConfig.AddProfile<DeviceNetworkToDeviceNetworkDtoProfile>();
                mapperConfig.AddProfile<DeviceBatterToDeviceBatteryDtoProfile>();
                mapperConfig.AddProfile<ServiceStatusToServiceStatusDtoProfile>();
                mapperConfig.AddProfile<ServiceStatusDtoToServiceStatusProfile>();
                mapperConfig.AddProfile<DeviceToDeviceSummaryDtoProfile>();
                mapperConfig.AddProfile<DockerServiceDtoToDockerServiceProfile>();
                mapperConfig.AddProfile<DeviceServiceDtoToDeviceServiceProfile>();
            });

            services.AddScoped<IDeviceService, DeviceService>();

            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Checkin.Api", Version = "v1" }));

            Log.Information("Using distributed cache");
            var redisSettings = Configuration.GetSection("Redis").Get<RedisProviderSettings>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(redisSettings.ConnectionString)
            );

            services.AddScoped<IDeviceCacheRepository, RedisDeviceCacheRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkin.Api v1"));
                app.UseCors("AllowAllOrigins");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
