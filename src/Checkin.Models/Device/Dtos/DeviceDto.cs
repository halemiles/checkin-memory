using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Moneyman.Models.Extensions;

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
        public DeviceServiceDto Services { get; set; }
        public DevicePower Power { get; set; }
        public DateTime CheckinDate {get; set;}
        public bool IsUp { get {return CheckinDate.IsUp();}}
        public IDictionary<string, object> Attributes {get; set;}
        public string ModifiedDateString { get; set; }
        public string CheckinDateString { get; set; }
    }
}