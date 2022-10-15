using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Checkin.Models;
using Checkin.Services;
using Checkin.Services.Interfaces;
using AutoMapper;

namespace Checkin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")] //TODO - change to /api/v1/device
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService deviceService;
        private readonly IMapper mapper;

        private readonly ILogger logger;

        public DeviceController(
            IDeviceService deviceService,
            IMapper mapper,
            ILogger logger
        )
        {
            this.deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("search")]
        public ActionResult Search(
            [FromQuery] int? deviceId,
            [FromQuery] string ipAddress
        )
        {
                return Ok(deviceService.Search(deviceId, ipAddress));
                //TODO - Return not found if no devices are found
        }

        [HttpGet]
        public ActionResult GetAll(string name)
        {
            var devices = deviceService.GetByKey($"device:{name}"); //tTODO - Use an extension
            //var deviceDto = mapper.Map<List<Device>, List<DeviceSummaryDto>>(devices);
            return Ok(devices);
            //TODO - Return not found if no devices are found
        }

        [HttpPost]
        public ActionResult CreateDevice([FromBody]DeviceDto deviceDto)
        {
            logger
                .ForContext("DeviceName",deviceDto.Name)
                .ForContext("HttpContext",HttpContext)
                .Debug("Creating Device");
            var device = mapper.Map<DeviceDto, Device>(deviceDto);
            deviceService.CreateOrUpdate(device);
            return Ok(); //TODO - Consider using CreatedAtRoute to return the created device
            //TODO - Return Internal error if the device is not created.
        }

        [HttpPut]
        public IActionResult UpdateDevice([FromBody] DeviceDto deviceDto)
        {
            var device = mapper.Map<DeviceDto, Device>(deviceDto);
            deviceService.CreateOrUpdate(device);
            return Ok();
            //TODO - Return internal error if the device is not updated.
        }

        public IActionResult DeleteDevice(string deviceName)
        {
            deviceService.Delete($"device:{deviceName.ToLower()}"); //TODO - Use an extension
            return Ok();
            //TODO - Return internal error if the device is not deleted.
        }
    }
}
