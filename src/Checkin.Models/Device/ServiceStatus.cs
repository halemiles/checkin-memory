using System.Collections.Generic;

namespace Checkin.Models
{
     public class ServiceStatus
    {
        public string Name { get; set; }
        public DeviceServiceStatus Status { get; set; }

        List<DockerService> DockerServices {get; set;}
    }
}