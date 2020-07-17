using System;

namespace Pocket.Extensions
{
    /// <summary>
    ///     Represents extension-methods for <see cref="IComparable{T}"/>.
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        ///     Checks whether <paramref name="self"/> is less than specified value.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="than">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is less than <paramref name="than"/>, otherwise <code>false</code>.</returns>
        public static bool IsLess<T>(this T self, T than) where T : IComparable<T> =>
            self.CompareTo(than) == -1;
        
        /// <summary>
        ///     Checks whether <paramref name="self"/> is less than or equal to specified value.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="than">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is less than or equal to <paramref name="than"/>, otherwise <code>false</code>.</returns>
        public static bool IsLessOrEqual<T>(this T self, T than) where T : IComparable<T> =>
            self.CompareTo(than) <= 0;
        
        /// <summary>
        ///     Checks whether <paramref name="self"/> is greater than specified value.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="than">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is greater than <paramref name="than"/>, otherwise <code>false</code>.</returns>
        public static bool IsGreater<T>(this T self, T than) where T : IComparable<T> =>
            self.CompareTo(than) == 1;
        
        /// <summary>
        ///     Checks whether <paramref name="self"/> is greater than or equal to specified value.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="than">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is greater than or equal to <paramref name="than"/>, otherwise <code>false</code>.</returns>
        public static bool IsGreaterOrEqual<T>(this T self, T than) where T : IComparable<T> =>
            self.CompareTo(than) >= 0;

        /// <summary>
        ///     Constraints the value to be below specified upper bound.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="than">Upper bound.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Either <paramref name="self"/> or <paramref name="than"/> if <paramref name="self"/> greater than <paramref name="than"/>.</returns>
        public static T ButNotGreater<T>(this T self, T than) where T : IComparable<T> =>
            self.IsGreater(than) ? than : self;
        
        /// <summary>
        ///     Constraints the value to be above specified lower bound.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="than">Lower bound.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Either <paramref name="self"/> or <paramref name="than"/> if <paramref name="self"/> less than <paramref name="than"/>.</returns>
        public static T ButNotLess<T>(this T self, T than) where T : IComparable<T> =>
            self.IsLess(than) ? than : self;
    }
}