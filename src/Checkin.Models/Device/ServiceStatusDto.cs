using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class ServiceStatusDto
    {
        public string Name { get; set; }
        public DeviceServiceStatus Status { get; set; }
    }
}