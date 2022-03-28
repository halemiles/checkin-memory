using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Checkin.Models;
using Checkin.Services;
using Checkin.Services.Interfaces;

namespace Checkin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService deviceService;

        private readonly ILogger<DeviceController> _logger;

        public DeviceController(
            IDeviceService deviceService
        )
        {
            this.deviceService = deviceService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(deviceService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateDevice([FromBody]DeviceDto deviceDto)
        {
            Device device = new()
            {
                Id = 99999, //deviceDto.Id,
                Name = deviceDto.Name,
                IpAddress = deviceDto.IpAddress,
                CreatedDate = deviceDto.CreatedDate,
                ModifiedDate = DateTime.Now
            };
            deviceService.Add(device);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateDevice([FromBody] DeviceDto deviceDto)
        {
            return Ok();
        }
    }
}
