using System.Collections.Generic;

namespace Checkin.Models
{
    public class DevicePowerDto
    {
        public string Type {get; set;} //TODO: Turn this into an enum
        public IEnumerable<DeviceBattery> Batteries {get; set;}
    }
}