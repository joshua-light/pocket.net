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
            bool isNotEmpty;

            if (self is IList<T> list)
                isNotEmpty = list.Count != 0;
            else if (self is ICollection<T> collection)
                isNotEmpty = collection.Count != 0;
            else
                isNotEmpty = self.Any();
            
            if (isNotEmpty)
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
            bool isEmpty;

            // TODO: Extract to separate method due to #14 issue.
            if (self is IList<T> list)
                isEmpty = list.Count == 0;
            else if (self is ICollection<T> collection)
                isEmpty = collection.Count == 0;
            else
                isEmpty = !self.Any();
            
            if (isEmpty)
                throw new ArgumentException("Specified collection is empty.");
        }
    }
}