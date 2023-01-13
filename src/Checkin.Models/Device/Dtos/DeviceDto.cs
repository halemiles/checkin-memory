using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class DeviceDto
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string IpAddress { get; set; }
        public DeviceNetworkDto ExternalNetwork { get; set; }
        public IEnumerable<ServiceStatusDto> Services { get; set; }
        public DevicePower Power { get; set; }
        public IDictionary<string, object> Attributes {get; set;}
        public string ModifiedDateString { get; set; }
    }
}