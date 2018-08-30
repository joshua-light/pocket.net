using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods with invariants checking for types of <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class GuardEnumerableExtensions
    {
        /// <summary>
        ///     Ensures that <paramref name="self"/> is empty collection.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of elements in list.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is empty.</exception>
        public static void EnsureEmpty<T>(this IEnumerable<T> self)
        {
            if (!self.IsEmpty())
                throw new ArgumentException("Specified collection is not empty.");
        }
        
        /// <summary>
        ///     Ensures that <paramref name="self"/> is not empty collection.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of elements in list.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is not empty.</exception>
        public static void EnsureNotEmpty<T>(this IEnumerable<T> self)
        {
            if (self.IsEmpty())
                throw new ArgumentException("Specified collection is empty.");
        }
    }
}