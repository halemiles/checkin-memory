using System;

namespace Checkin.Models.Extensions
{
    public static class PrettyDate
    {
        public static string GetPrettyDate(this DateTime d)
        {
            return GetPrettyDate(d, DateTime.Now);
        }
        
        public static string GetPrettyDate(this DateTime d, DateTime? nowOverride = null)
        {
            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = nowOverride?.Subtract(d) ?? DateTime.Now.Subtract(d);
            
            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;
            
            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;
            Console.WriteLine(dayDiff);
            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }
            
            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                if (secDiff < 10)
                {
                    return "just now";
                }
                // A.
                // Less than 30 seconds ago.
                if (secDiff < 30)
                {
                    return "30 seconds ago";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                    dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format("{0} weeks ago",
                    Math.Ceiling((double)dayDiff / 7));
            }
            return null;
        }
    }
}