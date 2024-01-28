using System.Collections.Specialized;
using System.Net;
using Checkin.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Checkin.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public WebhookService(
            ILogger logger,
            IConfiguration configuration
        )
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        public void SendMessage(string message)
        {
            var hookUrl = "";
            sendDiscordWebhook(hookUrl, null, "Test person", message);
            
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