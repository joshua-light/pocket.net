using System;

namespace Pocket.Common
{
    public static class DayOfWeekExtensions
    {
        public static int DaysTo(this DayOfWeek self, DayOfWeek other)
        {
            const int daysInWeek = 7;
      
            var diff = other - self;
            if (diff >= 0)
                return diff;

            return daysInWeek + diff;
        }
    }
}