namespace Pocket.Time
{
    public static class ClockExtensions
    {
        public static IClock Frozen(this IClock self) =>
            new FrozenClock(self.Time);
        
        public static TimeMark Watch(this IClock self) =>
            new TimeMark(self);
    }
}