using System;
using System.Collections.Generic;

namespace Checkin.Models
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public DeviceNetwork Network { get; set; }
        
        
        public DateTime CreatedDate {get; set;}
        public IDictionary<string, object> Attributes {get; set;}
    }
}