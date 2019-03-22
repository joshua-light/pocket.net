using System;

namespace Pocket.Common.Time
{
  public sealed class FrozenClock : IClock
  {
    public FrozenClock(DateTime time) => Time = time;
        
    public DateTime Time { get; }
  }
}