using System;

namespace Checkin.Models
{
    public class DeviceNetwork
    {
        public string InternalIpAddress { get; set; }
        public string DomainName { get; set; }
        public string IspName {get; set;}
        public DateTime LastModified {get; set;}
        public DateTime DateCreated {get; set;}
        
        
    }
}