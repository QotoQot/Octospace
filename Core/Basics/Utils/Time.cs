using System;
namespace Core.Basics.Utils
{
    public static class Time
    {
        static readonly DateTime UnixStartDate = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long UnixNow
        {
            get
            {
                TimeSpan timeSpan = DateTime.UtcNow - UnixStartDate;
                return (long)timeSpan.TotalSeconds;
            }
        }

        // replace with Humanizer
        //public static string TimeStringFromSeconds(float totalSeconds, bool showHours)
        //{
        //    int minutes = (int)((totalSeconds % 3600) / 60);
        //    int seconds = (int)(totalSeconds % 60);

        //    string timeString = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        //    if (showHours)
        //        timeString = ((int)(totalSeconds / 3600)).ToString("D2") + ":" + timeString;

        //    return timeString;
        //}
    }
}
