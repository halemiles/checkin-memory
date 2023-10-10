using System;

namespace Moneyman.Models.Extensions
{
    public static class DeviceServiceExtensions
    {
        public static bool IsUp(this DateTime checkinDate)
        {
            int limit = 10;
            var checkinLimit = DateTime.Now.AddMinutes(-limit); //.AddSeconds(-1);
            if(checkinDate >= checkinLimit)
            {
                return true;
            }

            return false;
        }
    }
}