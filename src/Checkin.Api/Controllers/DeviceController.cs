﻿using System;
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
    [Route("[controller]")]
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
            this.deviceService = deviceService ?? throw new ArguementNullException(nameof(deviceService))
            this.mapper = mapper ?? throw new ArguementNullException(nameof(mapper));
            this.logger = logger ?? throw new ArguementNullException(nameof(logger));
        }

        [HttpGet("search")]
        public ActionResult Search(
            [FromQuery] int? deviceId,
            [FromQuery] string ipAddress
        )
        {
                return Ok(deviceService.Search(deviceId, ipAddress));
        }

        [HttpGet]
        public ActionResult GetAll()
        {
                return Ok(deviceService.GetAll());
        }

        [HttpPost]
        public ActionResult CreateDevice([FromBody]DeviceDto deviceDto)
        {
            logger
                .ForContext("DeviceName",deviceDto.Name)
                .ForContext("HttpContext",HttpContext)
                .Debug("Creating Device");
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
            deviceService.Delete(id);
            return Ok();
        }
    }
}
