using System;

namespace Pocket.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime time) => time.StartOfWeek(whenFirstDayIs: DayOfWeek.Monday);
        public static DateTime StartOfWeek(this DateTime time, DayOfWeek whenFirstDayIs)
        {
            var days =  7 - time.DayOfWeek.DaysTo(whenFirstDayIs);
            if (days == 7)
                return time.Date;
      
            return time.AddDays(-days).Date;
        }
    }
}