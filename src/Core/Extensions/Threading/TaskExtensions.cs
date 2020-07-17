using System;
using System.Threading.Tasks;

namespace Pocket.Extensions
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
        /// <param name="ms">Amount of time to wait.</param>
        /// <typeparam name="T">Type of task result.</typeparam>
        /// <returns><code>self.Result</code> if task is completed within <paramref name="ms"/>, otherwise <code>null</code>.</returns>
        public static async Task<T> WithTimeout<T>(this Task<T> self, int ms)
        {
            var timeout = Task.Delay(ms);
            
            await Task.WhenAny(self, timeout);

            return timeout.IsCompleted && !self.IsCompleted
                ? default
                : self.Result;
        }
        
        public static Task<V> Then<T, V>(this Task<T> self, Func<T, V> map) =>
            self.ContinueWith(x => map(x.Result));
        public static Task<V> Then<T, V>(this Task<T> self, Func<T, Task<V>> map) =>
            self.ContinueWith(x => map(x.Result)).Unwrap();
        
        public static Task<V> With<T, V>(this Task<T> self, Func<T, V> map) =>
            self.ContinueWith(x => map(x.Result));
        public static Task<V> With<T, V>(this Task<T> self, Func<T, Task<V>> map) =>
            self.ContinueWith(x => map(x.Result)).Unwrap();

        public static Task<T> Or<T>(this Task<T> self, T @default) =>
            self.ContinueWith(x => x.IsFaulted ? @default : x.Result);
        public static Task<T[]> OrEmpty<T>(this Task<T[]> self) =>
            self.Or(Array.Empty<T>());
    }
}