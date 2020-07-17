using System;

namespace Pocket.Time
{
    public sealed class UtcNowClock : IClock
    {
        public DateTime Time => DateTime.UtcNow;
    }
}