using System;

namespace Pocket.Time
{
  public sealed class FrozenClock : IClock
  {
    public FrozenClock(DateTime time) => Time = time;
        
    public DateTime Time { get; }
  }
}