using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Checkin.Api.Extensions;
using Serilog;

namespace Checkin.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseCustomTelemetry();
                });
    }
}
