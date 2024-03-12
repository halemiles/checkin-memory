using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkin.Models;
using Checkin.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Checkin.Services
{
    public class TimedHostedService : IHostedService
    {   

        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly IAlertService alertService;
        private readonly IWebhookService webhookService;
        private readonly IDeviceService deviceService;
        private Timer? _timer = null;

        public TimedHostedService(ILogger logger,
            IAlertService alertService,
            IWebhookService webhookService,
            IDeviceService deviceService
        )
        {
            _logger = logger;
            this.alertService = alertService;
            this.webhookService = webhookService;
            this.deviceService = deviceService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);
            List<DeviceRule> rules = new()
            {
                new DeviceRule(){
                    
                }
            };
            var devices = deviceService.GetAll().Payload;
            foreach(var device in devices)
            {
                var result = alertService.CheckAlerts(device, rules);
                webhookService.SendMessage(result);
                _logger.Information(result);
                _logger.Information(
                    "Timed Hosted Service is working. Count: {Count}", count);
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }



        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}