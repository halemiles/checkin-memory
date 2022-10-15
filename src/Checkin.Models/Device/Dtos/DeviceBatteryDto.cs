using System;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class DeviceBatteryDto
    {
        public string Name { get; set; }
        public int BatteryLevel { get; set; }
    }
}