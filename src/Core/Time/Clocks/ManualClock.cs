using System;

namespace Pocket.Common.Time
{
    public sealed class ManualClock : IClock
    {
        public ManualClock() : this(DateTime.UtcNow) { }
        public ManualClock(DateTime now) =>
            Time = now;
        
        public DateTime Time { get; private set; }

        public void Skip(int ms) =>
            Time = Time.AddMilliseconds(ms);
    }
}