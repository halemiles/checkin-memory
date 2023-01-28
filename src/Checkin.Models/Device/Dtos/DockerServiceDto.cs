using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class DockerServiceDto
    {
        public string ContainerName { get; set; }
        public int Port { get; set; }
        public string Status { get; set; }
    }
}