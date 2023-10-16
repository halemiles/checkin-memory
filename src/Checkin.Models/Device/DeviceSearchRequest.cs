using System;

namespace Checkin.Models
{
    public class DeviceSearchRequest
    {
        public Guid? DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string IpAddress { get; set; }
        public bool? IsUp { get; set; }
    }
}