using System;
using static Pocket.Common.Guard;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods with functional style for generic types.
    /// </summary>
    public static class FunctionalExtensions
    {
        /// <summary>
        ///     Represents specified object as object of other type using specified <paramref name="map"/> function.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="map">Function that maps <paramref name="self"/> to object of type <typeparamref name="TResult"/>.</param>
        /// <typeparam name="TInput">Type of object to be represented.</typeparam>
        /// <typeparam name="TResult">Type of result object that will represent <paramref name="self"/>.</typeparam>
        /// <returns>Representation of <paramref name="self"/> in <typeparamref name="TResult"/> type.</returns>
        public static TResult As<TInput, TResult>(this TInput self, Func<TInput, TResult> map)
        {
            Ensure(map).NotNull();
            return map(self);
        }

        /// <summary>
        ///     Performs specified <see cref="Action"/> function on object and returns this object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="apply">Function that will be applied on <paramref name="self"/> object.</param>
        /// <typeparam name="T">Type of <paramref name="self"/> object.</typeparam>
        /// <returns><code>this</code> object.</returns>
        public static T Do<T>(this T self, Action<T> apply)
        {
            apply?.Invoke(self);
            return self;
        }
    }
}