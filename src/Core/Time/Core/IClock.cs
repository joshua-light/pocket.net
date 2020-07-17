using System;

namespace Pocket.Time
{
    public interface IClock
    {
        DateTime Time { get; }
    }
}