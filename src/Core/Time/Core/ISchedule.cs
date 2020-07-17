namespace Pocket.Time
{
    public interface ISchedule
    {
        IPromise Wait(int ms);
    }
}