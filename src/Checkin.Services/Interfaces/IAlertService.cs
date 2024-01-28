using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;
using Checkin.Models.Dto;

namespace Checkin.Services.Interfaces
{
    public interface IAlertService
    {
        public string CheckAlerts(DeviceDto device, List<DeviceRule> rules);
    }
}