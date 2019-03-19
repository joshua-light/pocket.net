using System;

namespace Pocket.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime time) => time.StartOfWeek(whenFirstDayIs: DayOfWeek.Monday);
        public static DateTime StartOfWeek(this DateTime time, DayOfWeek whenFirstDayIs)
        {
            const int daysInWeek = 7;
            
            var days =  daysInWeek - time.DayOfWeek.DaysTo(whenFirstDayIs);
            if (days == daysInWeek)
                return time.Date;
      
            return time.AddDays(-days).Date;
        }
    }
}