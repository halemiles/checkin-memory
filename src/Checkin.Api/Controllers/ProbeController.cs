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
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Api.Controllers
{
    [ExcludeFromCodeCoverage]
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
    }
}
