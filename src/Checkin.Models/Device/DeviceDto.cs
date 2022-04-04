using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Checkin.Models
{
    public class DeviceDto
    {

        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string IpAddress { get; set; }
        public DeviceNetworkDto ExternalNetwork { get; set; }
        public DateTime CreatedDate {get; set;}
        public IDictionary<string, object> Attributes {get; set;}
    }
}