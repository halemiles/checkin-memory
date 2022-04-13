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
    public class ProbeController : ControllerBase
    {

        private readonly ILogger<ProbeController> logger;
        public ProbeController(
            ILogger<ProbeController> logger
        )
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetVersion()
        {
            logger.LogInformation("Accessing version");
            return Ok("v0.1");
        }

        [HttpGet("exception")]
        public IActionResult SimulateException()
        {
            logger.LogInformation("Simulating exception...");
            throw new Exception("Test exception");
        }
    }
}
