namespace Pocket.Time
{
    public class FakeSchedule : ISchedule
    {
        public IPromise Wait(int ms) => Promise.Fake;
    }
}