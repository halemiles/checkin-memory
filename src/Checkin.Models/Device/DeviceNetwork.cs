using System;

namespace Checkin.Models
{
    public class DeviceNetwork
    {
        public string ExternalIpAddress { get; set; }
        public string IspName {get; set;}
        public DateTime LastModified {get; set;}
        public DateTime DateCreated {get; set;}
    }
}