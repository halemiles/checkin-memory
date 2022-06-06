namespace Checkin.Api.Models
{
    public class SeqSettings
    {
        public bool UseSeq
        {
            get
            {
                return !string.IsNullOrEmpty(Host);
            }
        }
        public string Host { get; set; }
        public string ApiKey { get; set; }
    }
}