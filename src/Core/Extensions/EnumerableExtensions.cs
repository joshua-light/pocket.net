using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Returns <see cref="Enumerable.Empty{TResult}"/>, if <code>this</code> is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of elements in <see cref="IEnumerable{T}"/>.</typeparam>
        /// <returns><see cref="Enumerable.Empty{T}"/> if <code>this</code> is <code>null</code>, otherwise returns <code>this</code>.</returns>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> self) => self ?? Enumerable.Empty<T>();

        /// <summary>
        ///     Takes first object from <see cref="IEnumerable{T}"/> that has minimum value, provided by <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in <see cref="IEnumerable{T}"/></typeparam>
        /// <typeparam name="TMin">Type of <see cref="IComparable{T}"/> element, that will be used for search.</typeparam>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="selector">Selector of <see cref="IComparable{T}"/> elements, that will be used for search.</param>
        /// <returns>First object, that has minimum value, provided by <paramref name="selector"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        public static T TakeMin<T, TMin>(this IEnumerable<T> self, Func<T, TMin> selector) where TMin : IComparable<TMin>
        {
            self.EnsureNotNull();
            selector.EnsureNotNull();
            
            var min = default(T);
            var first = true;

            foreach (var item in self)
            {
                if (first)
                {
                    min = item;
                    first = false;
                }
                else if (selector(item).CompareTo(selector(min)) < 0)
                {
                    min = item;
                }
            }

            return min;
        }

        /// <summary>
        ///     Takes first object from <see cref="IEnumerable{T}"/> that has maximum value, provided by <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in <see cref="IEnumerable{T}"/></typeparam>
        /// <typeparam name="TMax">Type of <see cref="IComparable{T}"/> element, that will be used for search.</typeparam>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="selector">Selector of <see cref="IComparable{T}"/> elements, that will be used for search.</param>
        /// <returns>First object, that has maximum value, provided by <paramref name="selector"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        public static T TakeMax<T, TMax>(this IEnumerable<T> self, Func<T, TMax> selector) where TMax : IComparable<TMax>
        {
            self.EnsureNotNull();
            selector.EnsureNotNull();
            
            var max = default(T);
            var first = true;

            foreach (var item in self)
            {
                if (first)
                {
                    max = item;
                    first = false;
                }
                else if (selector(item).CompareTo(selector(max)) > 0)
                {
                    max = item;
                }
            }

            return max;
        }
    }
}