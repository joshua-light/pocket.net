using System;

namespace Pocket.Time
{
  public class NowClock : IClock
  {
    public DateTime Time => DateTime.Now;
  }
}