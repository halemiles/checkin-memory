using System.Diagnostics.CodeAnalysis;

namespace Checkin.Api.Models
{
    [ExcludeFromCodeCoverage]
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