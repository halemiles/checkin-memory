using System;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class DeviceNetworkDto
    {
        public string ExternalIpAddress { get; set; }
        public string DomainName { get; set; }
        public string IspName {get; set;}
        public DateTime LastModified {get; set;}
        public DateTime DateCreated {get; set;}
    }
}