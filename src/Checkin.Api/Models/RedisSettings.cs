using System.Diagnostics.CodeAnalysis;

namespace Checkin.Api.Models
{
    [ExcludeFromCodeCoverage]
    internal class RedisProviderSettings
    {
        public string ConnectionString {get; set;}
        public string InstanceName { get; set; }
    }
}