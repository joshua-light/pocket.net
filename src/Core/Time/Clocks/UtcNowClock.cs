using System;

namespace Pocket.Common.Time
{
    public sealed class UtcNowClock : IClock
    {
        public DateTime Time => DateTime.UtcNow;
    }
}