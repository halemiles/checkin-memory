using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Models
{
    [ExcludeFromCodeCoverage]
    public class DeviceSummaryDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string ModifiedDateString { get; set; }
        public string IpAddress { get; set; }
        public int BatteryLevel { get; set; }
    }
}