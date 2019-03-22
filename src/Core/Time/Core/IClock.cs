using System;

namespace Pocket.Common.Time
{
    public interface IClock
    {
        DateTime Time { get; }
    }
}