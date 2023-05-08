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
            [FromQuery] Guid? deviceId,
            [FromQuery] string ipAddress,
            [FromQuery] string name,
            [FromQuery] bool? isUp
        )
        {
            var result = deviceService.Search(deviceId, ipAddress, name, isUp);

              

            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetAll(string name)
        {
            var devices = deviceService.GetAll(); //TODO - Return the DeviceDto

            return Ok(devices);
            //TODO - Return not found if no devices are found
        }

        [HttpGet("summary")]
        public ActionResult Summary(string name)
        {
            var devices = deviceService.GetAll();
            var mappedSummary = devices.Payload.Select(x => mapper.Map<DeviceDto, DeviceSummaryDto>(x));
            return Ok(mappedSummary);
        }

        [HttpPost]
        public ActionResult CreateDevice([FromBody]DeviceDto deviceDto)
        {
            logger
                .ForContext("DeviceName",deviceDto.Name)
                .ForContext("HttpContext",HttpContext)
                .Debug("Creating Device");
            try
            {
                var device = mapper.Map<DeviceDto, Device>(deviceDto);
                deviceService.CreateOrUpdate(device);
            }    
            catch(Exception ex)
            {
                logger
                    .Fatal(ex.ToString());
                
                return StatusCode(500);
            }
            
            
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateDevice([FromBody] DeviceDto deviceDto)
        {
             try
            {
                var device = mapper.Map<DeviceDto, Device>(deviceDto);
                deviceService.CreateOrUpdate(device);
            }    
            catch(Exception ex)
            {
                logger
                    .Fatal(ex.ToString());
                
                return StatusCode(500);
            }

            
            return Ok();
            //TODO - Return internal error if the device is not updated.
        }

        public IActionResult DeleteDevice(string deviceName)
        {
            deviceService.Delete(deviceName); //TODO - Use an extension
            return Ok();
            //TODO - Return internal error if the device is not deleted.
            //TODO - Return not found if the device does not exist
        }
    }
}
