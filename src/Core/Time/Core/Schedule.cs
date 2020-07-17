namespace Pocket.Time
{
    public static class Schedule
    {
        public static readonly ManualSchedule Manual = new ManualSchedule();
        public static readonly ISchedule Async = new AsyncSchedule();
    }
}