namespace Checkin.Api.Models
{
    public class SentrySettings
    {
        public bool UseSentry
        {
            get
            {
                return !string.IsNullOrEmpty(Dsn);
            }
        }
        public string Dsn { get; set; }
    }
}