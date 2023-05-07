using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Checkin.Models;
using Checkin.Services;
using Checkin.Services.Interfaces;
using AutoMapper;
using Prometheus;

namespace Checkin.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MetricsController : ControllerBase
    {

        public MetricsController()
        {

        }

         private readonly List<Device> _devices = new List<Device>();

        private readonly Counter _deviceCount = Metrics.CreateCounter("device_count", "Number of devices");

        private readonly Gauge _devicePower = Metrics.CreateGauge("device_power", "Power consumption of devices");

        private readonly Gauge _deviceAttribute = Metrics.CreateGauge("device_attribute", "Device attribute");


        [HttpGet]
        public IActionResult Index()
        {
            Counter ProcessedJobCount = Metrics
                .CreateCounter("myapp_jobs_processed_total", "Number of processed jobs.");


        
            ProcessedJobCount.Inc();
            return Ok();
        }

    
    }
}
