using System;
using System.Collections.Generic;
using static Pocket.Guard;

namespace Pocket.Extensions
{
    /// <summary>
    ///     Represents extension-methods for <see cref="ICollection{T}"/>.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Adds elements of the specified collection <paramref name="other"/> to the end of <paramref name="self"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">The collection whose elements should be added to the end of <paramref name="self"/>.</param>
        /// <typeparam name="T">Type of elements in <see cref="ICollection{T}"/>.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="self"/> or <paramref name="other"/> is <code>null</code>.</exception>
        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> other)
        {
            Ensure(that: self).NotNull();
            Ensure(that: other).NotNull();

            if (other is List<T> list)
                foreach (var x in list)
                    self.Add(x);
            else
                foreach (var x in other)
                    self.Add(x);
        }
    }
}
