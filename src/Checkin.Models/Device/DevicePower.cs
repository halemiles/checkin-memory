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
        public PowerType Type => PowerType.MAINS;
        public IEnumerable<DeviceBattery> Batteries { get; set; }
    }
}