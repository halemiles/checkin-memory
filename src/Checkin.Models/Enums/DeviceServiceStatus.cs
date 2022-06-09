using System.ComponentModel;

namespace Checkin.Models
{
    public enum DeviceServiceStatus
    {
        [Description("Unknown")]
        UNKNOWN = 0,
        [Description("Running")]
        UP = 1,
        [Description("Stopped")]
        DOWN = 2
    }
}