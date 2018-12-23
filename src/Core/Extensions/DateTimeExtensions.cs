using System;

namespace Pocket.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime time)
        {
            const int daysInWeek = 7;
            
            var days =  daysInWeek - time.DayOfWeek.DaysTo(DayOfWeek.Monday);
            if (days == daysInWeek)
                return time.Date;
      
            return time.AddDays(-days).Date;
        }
    }
}