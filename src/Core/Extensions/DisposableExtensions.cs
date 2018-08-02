using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="IDisposable"/>.
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        ///     Uses <see cref="IDisposable"/> object to produce other object through <paramref name="map"/> function.
        ///     Then disposes.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="map">Function that maps <see cref="IDisposable"/> object to object of type <typeparamref name="TOut"/>.</param>
        /// <typeparam name="TIn">Type of <see cref="IDisposable"/> object.</typeparam>
        /// <typeparam name="TOut">Type of result object.</typeparam>
        /// <returns></returns>
        public static TOut Using<TIn, TOut>(this TIn self, Func<TIn, TOut> map) where TIn : IDisposable
        {
            map.EnsureNotNull();

            return self
                .As(map)
                .Do(_ => self.Dispose());
        }
    }
}