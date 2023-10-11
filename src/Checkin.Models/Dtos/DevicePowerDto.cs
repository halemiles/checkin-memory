using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class DevicePowerDto
    {
        public string Type {get; set;} //TODO: Turn this into an enum
        public IEnumerable<DeviceBattery> Batteries {get; set;}
    }
}