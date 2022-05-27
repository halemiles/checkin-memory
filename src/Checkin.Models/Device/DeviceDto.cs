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
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string IpAddress { get; set; }
        public DeviceNetworkDto ExternalNetwork { get; set; }
        public IEnumerable<ServiceStatus> Services { get; set; }
        public IEnumerable<DeviceBattery> Batteries { get; set; }
        public DateTime CreatedDate {get; set;}
        public IDictionary<string, object> Attributes {get; set;}
    }
}