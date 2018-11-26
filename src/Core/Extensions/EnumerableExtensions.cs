using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

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
        ///     Performs some action on each element of <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of element in <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="onEach">Action, that will be performed on each element of <see cref="IEnumerable{T}"/>.</param>
        /// <returns>Source <see cref="IEnumerable{T}"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> self, Action<T> onEach)
        {
            self.EnsureNotNull();

            return EachIterator(self, onEach);
        }

        private static IEnumerable<T> EachIterator<T>(IEnumerable<T> source, Action<T> onEach)
        {
            foreach (var item in source)
            {
                onEach(item);
                yield return item;
            }
        }

        /// <summary>
        ///     Performs some action on each element of <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of element in <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="onEach">Action, that will be performed on each element of <see cref="IEnumerable{T}"/>.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> onEach)
        {
            self.EnsureNotNull();

            foreach (var item in self)
                onEach(item);
        }

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

        /// <summary>
        ///     Checks whether <paramref name="self"/> is <code>null</code> or contains no elements.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns><code>true</code> if <paramref name="self"/> is <code>null</code> or empty, otherwise — <code>false</code>.</returns>
        public static bool IsNullOrEmpty<T>([NoEnumeration] this IEnumerable<T> self) =>
            self == null || self.IsEmpty();

        /// <summary>
        ///     Checks whether <paramref name="self"/> contains no elements.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns><code>true</code> if <paramref name="self"/> is empty, otherwise — <code>false</code>.</returns>
        public static bool IsEmpty<T>([NoEnumeration] this IEnumerable<T> self)
        {
            if (self is IList<T> list)
                return list.Count == 0;
            
            if (self is ICollection<T> collection)
                return collection.Count == 0;
            
            return !self.Any();
        }

        /// <summary>
        ///     Produces the set difference of two sequences by using specified comparer function to compare values.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">
        ///     An IEnumerable{T} whose elements that also occur in the first sequence
        ///     will cause those elements to be removed from the returned sequence.
        /// </param>
        /// <param name="comparer">An IEqualityComparer{T} to compare values.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> self, IEnumerable<T> other, Func<T, T, bool> comparer) =>
            self.Except(other, new FuncAsEqualityComparer<T>(comparer));

        /// <summary>
        ///     Returns distinct elements from a sequence by using specified comparer function to compare values.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="comparer">An IEqualityComparer{T} to compare values.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns>An IEnumerable{T} that contains distinct elements from the source sequence.</returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> self, Func<T, T, bool> comparer) =>
            self.Distinct(new FuncAsEqualityComparer<T>(comparer));

        public static T One<T>(this IEnumerable<T> self, Func<T, bool> predicate, string orThrow = null)
        {
            var item = self.FirstOrDefault(predicate);
            if (item == null)
                throw new InvalidOperationException(orThrow ?? "Couldn't find item that matches specified predicate.");
            
            return item;
        }
    }
}