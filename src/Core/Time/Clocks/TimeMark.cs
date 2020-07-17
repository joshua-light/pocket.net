using System;

namespace Pocket.Time
{
    public struct TimeMark
    {
        public static readonly TimeMark Zero = new TimeMark();
        
        private readonly IClock _clock;
        private readonly DateTime _start;

        public TimeMark(IClock clock)
        {
            _clock = clock;
            _start = clock.Time;
        }

        public TimeSpan Elapsed() =>
            (_clock?.Time - _start).Or(TimeSpan.Zero);
    }
}