using System;

namespace Checkin.Models
{
    public class DeviceNetworkDto
    {
        public string ExternalIpAddress { get; set; }
        public string DomainName { get; set; }
        public string IspName {get; set;}
        public DateTime LastModified {get; set;}
        public DateTime DateCreated {get; set;}
    }
}