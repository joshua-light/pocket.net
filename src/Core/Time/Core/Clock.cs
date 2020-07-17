namespace Pocket.Time
{
  public static class Clock
  {
    public static class Frozen
    {
      public static readonly IClock Now =
        Clock.UtcNow.Frozen();
      public static readonly IClock UtcNow =
        Clock.Now.Frozen();
    }
    
    public static readonly IClock Now = new NowClock();
    public static readonly IClock UtcNow = new UtcNowClock();
  }
}