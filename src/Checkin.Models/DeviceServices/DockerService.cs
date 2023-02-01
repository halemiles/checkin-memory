using System.Collections.Generic;

namespace Checkin.Models
{
    public class DockerService
    {
        public string ContainerName { get; set; }
        public List<DockerPort> Ports { get; set; }
        public string Status {get; set;}
        
    }
}