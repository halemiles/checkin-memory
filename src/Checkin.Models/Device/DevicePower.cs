using System.Collections.Generic;

namespace Checkin.Models
{
    //TODO: Move this into its own file
    public enum PowerType
    {
        MAINS,
        BATTERY
    }

    public class DevicePower
    {
        public string Type {get; set;} //TODO: Turn this into an enum
        public IEnumerable<DeviceBattery> Batteries { get; set; }
    }
}