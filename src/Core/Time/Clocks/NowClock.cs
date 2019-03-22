using System;

namespace Pocket.Common.Time
{
  public class NowClock : IClock
  {
    public DateTime Time => DateTime.Now;
  }
}