using System;

namespace Moneyman.Services.Extensions
{
    public static class DeviceServiceExtensions
    {
        public static bool IsUp(this DateTime checkinDate)
        {
            var limit = DateTime.Now.AddMinutes(-5).AddSeconds(-1);
            if(checkinDate >= limit)
            {
                return true;
            }

            return false;
        }
    }
}