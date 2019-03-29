using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static Pocket.Common.Guard;

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
            Ensure(self).NotNull();

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
            Ensure(self).NotNull();

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
            Ensure(self).NotNull();
            Ensure(selector).NotNull();
            
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
            Ensure(self).NotNull();
            Ensure(selector).NotNull();
            
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

        public static T One<T>(this IEnumerable<T> self, Func<T, bool> predicate) =>
            self.FirstOrDefault(predicate);

        /// <summary>
        ///     Returns the first element of the sequence that satisfies a condition or throws exception with specified message.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="predicate">A function that represents a condition, which will be applied to elements of sequence.</param>
        /// <param name="orThrow">Message that will be used to throw exception if no element will satisfy a condition.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns>First element of the sequence that satisfies a condition.</returns>
        /// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.</exception>
        public static T One<T>(this IEnumerable<T> self, Func<T, bool> predicate, string orThrow)
        {
            var item = self.One(predicate);
            if (item == null && orThrow != null)
                throw new InvalidOperationException(orThrow);
            
            return item;
        }

        /// <summary>
        ///     Returns the second element of the sequence.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns>Second element of the sequence.</returns>
        /// <exception cref="InvalidOperationException">Sequence is empty or contains one element.</exception>
        public static T Second<T>(this IEnumerable<T> self) =>
            self.Skip(1).First();

        /// <summary>
        ///     Returns the previous element to <paramref name="item"/>. 
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="item">Item, the previous one to which will be returned.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns>The previous to <paramref name="item"/>.</returns>
        public static T PreviousTo<T>(this IEnumerable<T> self, T item)
        {
            var previous = default(T);

            foreach (var element in self)
            {
                if (EqualityComparer<T>.Default.Equals(element, item))
                    return previous;

                previous = element;
            }

            return previous;
        }
        
        /// <summary>
        ///     Returns the next element to <paramref name="item"/>. 
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="item">Item, the next one to which will be returned.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns>The next to <paramref name="item"/>.</returns>
        public static T NextTo<T>(this IEnumerable<T> self, T item)
        {
            var @return = false;

            foreach (var element in self)
            {
                if (EqualityComparer<T>.Default.Equals(element, item))
                {
                    @return = true;
                    continue;
                }

                if (@return)
                     return element;
            }

            return default;
        }

        /// <summary>
        ///     Concatenates the members of a sequence, using specified string as separator.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="with">String that will be inserted between items of <paramref name="self"/>.</param>
        /// <typeparam name="T">Type of elements in sequence.</typeparam>
        /// <returns>Items of <paramref name="self"/> converted to string and separated with <paramref name="with"/>.</returns>
        public static string Separated<T>(this IEnumerable<T> self, string with) =>
            string.Join(with, self);
        
        /// <summary>
        ///     Concatenates the members of a sequence, using specified string as separator.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="with">String that will be inserted between items of <paramref name="self"/>.</param>
        /// <returns>Items of <paramref name="self"/> converted to string and separated with <paramref name="with"/>.</returns>
        public static string Separated(this IEnumerable<string> self, string with) =>
            string.Join(with, self);
    }
}