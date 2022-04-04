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

namespace Checkin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService deviceService;
        private readonly IMapper mapper;

        private readonly ILogger<DeviceController> _logger;

        public DeviceController(
            IDeviceService deviceService,
            IMapper mapper
        )
        {
            this.deviceService = deviceService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(deviceService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateDevice([FromBody]DeviceDto deviceDto)
        {
            var device = mapper.Map<Device>(deviceDto);
            deviceService.Add(device);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateDevice([FromBody] DeviceDto deviceDto)
        {
            var device = mapper.Map<Device>(deviceDto);
            deviceService.Update(device);
            return Ok();
        }

        public IActionResult DeleteDevice(int id)
        {
            return Ok();
        }
    }
}
