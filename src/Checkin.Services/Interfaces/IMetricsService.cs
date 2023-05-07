using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IMetricsService
    {
        void RecordDeviceMetrics(Device device);
    }
}