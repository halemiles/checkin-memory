using System.Diagnostics.CodeAnalysis;

namespace Checkin.Api
{
    [ExcludeFromCodeCoverage]
    internal class ApplicationInsightsSettings
    {
        public string ConnectionString { get; set; }
    }
}