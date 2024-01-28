namespace Checkin.Services.Interfaces
{
    public interface IWebhookService
    {
        public void SendMessage(string message);
    }
}