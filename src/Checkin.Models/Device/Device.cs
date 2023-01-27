using System;
using System.Collections.Generic;
using Checkin.Models.Extensions;

namespace Checkin.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Key { get { return $"device:{Name?.ToLower() ?? string.Empty }"; } }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public DeviceNetwork ExternalNetwork { get; set; }
        public DeviceServices Services { get; set; }
        public DevicePower Power {get; set;}
        public DateTime CreatedDate {get; set;}
        public DateTime ModifiedDate {get; set;}
        public string ModifiedDateString
        {
            get { return ModifiedDate.GetPrettyDate(); }
        }
    }
}