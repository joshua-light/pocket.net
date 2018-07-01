using System.Threading.Tasks;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="Task{TResult}"/>.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///     Waits until <paramref name="self"/> task is completed or for specified amount of time.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="timeoutMs">Amount of time to wait.</param>
        /// <typeparam name="T">Type of task result.</typeparam>
        /// <returns><code>self.Result</code> if task is completed within <paramref name="timeoutMs"/>, otherwise <code>null</code>.</returns>
        public static async Task<T> WithTimeout<T>(this Task<T> self, int timeoutMs)
        {
            var timeout = Task.Delay(timeoutMs);
            
            await Task.WhenAny(self, timeout);

            return timeout.IsCompleted && !self.IsCompleted
                ? default
                : self.Result;
        }
        
        /// <summary>
        ///     Waits until <paramref name="self"/> task with <see cref="Result"/> is completed or for specified amount of time.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="timeoutMs">Amount of time to wait.</param>
        /// <returns><code>self.Result</code> if task is completed within <paramref name="timeoutMs"/>, otherwise <code>null</code>.</returns>
        public static async Task<Result> WithTimeout(this Task<Result> self, int timeoutMs)
        {
            var timeout = Task.Delay(timeoutMs);
            
            await Task.WhenAny(self, timeout);

            return timeout.IsCompleted && !self.IsCompleted
                ? Result.Failed($"Failed by timeout: {timeoutMs}.")
                : self.Result;
        }
        
        /// <summary>
        ///     Waits until <paramref name="self"/> task with <see cref="Result{T}"/> is completed or for specified amount of time.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="timeoutMs">Amount of time to wait.</param>
        /// <typeparam name="T">Type of task result.</typeparam>
        /// <returns><code>self.Result</code> if task is completed within <paramref name="timeoutMs"/>, otherwise <code>null</code>.</returns>
        public static async Task<Result<T>> WithTimeout<T>(this Task<Result<T>> self, int timeoutMs)
        {
            var timeout = Task.Delay(timeoutMs);
            
            await Task.WhenAny(self, timeout);

            return timeout.IsCompleted && !self.IsCompleted
                ? Result.Failed<T>($"Failed by timeout: {timeoutMs}.")
                : self.Result;
        }
    }
}