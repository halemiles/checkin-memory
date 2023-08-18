using System;

namespace Checkin.Models.Extensions
{
    public static class StatusExtensions
    {
        public static string GetStatus(this DateTime lastCheckinDate)
        {
            var minutesElapsed = (lastCheckinDate - DateTime.Now).Minutes;
            var result = "OK";

            if (minutesElapsed > 5)
            {
                result = "Down";
            }
            return result;
        }
    }
}