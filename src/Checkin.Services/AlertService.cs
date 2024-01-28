using System;
using System.Collections.Generic;
using Checkin.Models;
using Checkin.Models.Dto;
using Checkin.Services.Interfaces;
using Serilog;

namespace Checkin.Services
{
    public class AlertService : IAlertService
    {
        private readonly ILogger logger;
        private readonly IWebhookService webhookService;

        public AlertService(
            ILogger logger,
            IWebhookService webhookService
        ){
            this.logger = logger;
            this.webhookService = webhookService;
        }

        public string CheckAlerts(DeviceDto device, List<DeviceRule> rules)
        {
            string message = string.Empty;

            DateTime currentTime = DateTime.UtcNow;
            if (device.CheckinDate < currentTime.AddMinutes(10))
            {
                message = "Device is down";
                logger.Information("Device is down");
                webhookService.SendMessage($"{device.Name} is down");
            }
            
            logger.Information("Device is whatever");
            return message;
        }
    }
}