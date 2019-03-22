using System.Threading.Tasks;

namespace Pocket.Common.Time
{
    public class AsyncSchedule : ISchedule
    {
        public IPromise Wait(int ms) =>
            new ManualPromise().Do(x => Satisfy(x, after: ms));

        private static async void Satisfy(ManualPromise promise, int after)
        {
            await Task.Delay(after);
            
            promise.Satisfy();
        }
    }
}