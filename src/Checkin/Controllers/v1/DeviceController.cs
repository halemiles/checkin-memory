using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkin.Models;
using Checkin.Services.Interfaces;

namespace Checkin.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService deviceService;

        public DeviceController(
            IDeviceService deviceService
        )
        {
            this.deviceService = deviceService;            
        }

        [HttpGet("index")]
        public IActionResult GetAll()
        {
            return Ok(deviceService.GetAll().AsEnumerable());
        }
    }
}
