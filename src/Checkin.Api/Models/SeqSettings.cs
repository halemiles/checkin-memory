using System.Diagnostics.CodeAnalysis;

namespace Checkin.Api.Models
{
    [ExcludeFromCodeCoverage]
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