using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Checkin.Models;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Specialized;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Checkin.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("[controller]")]
    public class ProbeController : ControllerBase
    {
        private readonly ILogger<ProbeController> logger;
        private readonly IConfiguration configuration;
        public ProbeController(
            ILogger<ProbeController> logger,
            IConfiguration configuration
        )
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public IActionResult GetVersion()
        {
            logger.LogInformation("Accessing version");
            return Ok("v0.1");
        }

        

        [HttpGet("testwebhook")]
        public IActionResult TestWebhook2()
        {
            var hookUrl = configuration.GetSection("NotificationSettings").Get<NotificationSettings>();
            sendDiscordWebhook(hookUrl.DiscordWebhookUrl, null, "Test person", "Test Message");
            return Ok();
        }

        private void sendDiscordWebhook(string URL, string profilepic, string username, string message)
        {
                NameValueCollection discordValues = new NameValueCollection();
                discordValues.Add("username", username);
                discordValues.Add("avatar_url", profilepic);
                discordValues.Add("content", message);
                new WebClient().UploadValues(URL, discordValues);
        }

    }
}
