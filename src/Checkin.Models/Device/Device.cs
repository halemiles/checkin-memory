using System;
using Checkin.Models.Extensions;

namespace Checkin.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public DeviceNetwork Network { get; set; }
        public DateTime CreatedDate {get; set;}
        public DateTime ModifiedDate {get; set;}
        public string ModifiedDateString
        {
            get { return ModifiedDate.GetPrettyDate(); }
        }
    }
}