
using Checkin.Services.Interfaces;
using Checkin.Models;
using AutoMapper;
using Serilog;
using System;
using Prometheus;

namespace Checkin.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly IDeviceCacheRepository deviceRepository;

        public MetricsService(
            IDeviceCacheRepository deviceRepository
        )
        {
            this.deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public void RecordDeviceMetrics(Device device)
        {
            Counter ProcessedJobCount = Metrics
                .CreateCounter($"device_{device.Name}_ip", device.IpAddress, labelNames: new[] {"Label1"});
            ProcessedJobCount.WithLabels(new[] {"Lab1"});
            // Gauge gauge1 = Metrics.CreateGauge("a_uage_1");
            // gauge1.Value(1001);
        }
    }
}