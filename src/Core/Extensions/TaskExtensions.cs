using System.Threading.Tasks;

namespace Pocket.Common
{
    public static class TaskExtensions
    {
        public static async Task<T> WithTimeout<T>(this Task<T> self, int timeoutMs)
        {
            var timeout = Task.Delay(timeoutMs);
            await Task.WhenAny(self, timeout);

            return timeout.IsCompleted && !self.IsCompleted
                ? default
                : self.Result;
        }
        
        public static async Task<Result> WithTimeout(this Task<Result> self, int timeoutMs)
        {
            var timeout = Task.Delay(timeoutMs);
            await Task.WhenAny(self, timeout);

            return timeout.IsCompleted && !self.IsCompleted
                ? Result.Failed("Failed by timeout: " + timeoutMs + ".")
                : self.Result;
        }
        
        public static async Task<Result<T>> WithTimeout<T>(this Task<Result<T>> self, int timeoutMs)
        {
            var timeout = Task.Delay(timeoutMs);
            await Task.WhenAny(self, timeout);

            return timeout.IsCompleted && !self.IsCompleted
                ? Result.Failed<T>("Failed by timeout: " + timeoutMs + ".")
                : self.Result;
        }
    }
}