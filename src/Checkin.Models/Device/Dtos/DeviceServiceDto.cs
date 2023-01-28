using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class DeviceServiceDto
    {
        public List<DockerServiceDto> DockerServices {get; set;}
    }
}