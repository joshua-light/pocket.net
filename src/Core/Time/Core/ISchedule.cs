namespace Pocket.Common.Time
{
    public interface ISchedule
    {
        IPromise Wait(int ms);
    }
}