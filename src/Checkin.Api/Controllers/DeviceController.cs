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

        private readonly ILogger<DeviceController> logger;

        public DeviceController(
            IDeviceService deviceService,
            IMapper mapper,
            ILogger<DeviceController> logger
        )
        {
            this.deviceService = deviceService; //TODO: Null ref check
            this.mapper = mapper; //TODO: Null ref check
            this.logger = logger; //TODO: Null ref check
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await deviceService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromBody]DeviceDto deviceDto)
        {
            logger.LogInformation("Creating Device");
            var device = mapper.Map<Device>(deviceDto);
            await deviceService.Add(device);
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
            deviceService.Delete(id);
            return Ok();
        }
    }
}
