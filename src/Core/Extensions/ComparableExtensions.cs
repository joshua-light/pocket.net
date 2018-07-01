using System;

namespace Pocket.Common
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
        /// <param name="other">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is less than <paramref name="other"/>, otherwise <code>false</code>.</returns>
        public static bool IsLess<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) == -1;
        
        /// <summary>
        ///     Checks whether <paramref name="self"/> is less than or equal to specified value.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is less than or equal to <paramref name="other"/>, otherwise <code>false</code>.</returns>
        public static bool IsLessOrEqual<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) <= 0;
        
        /// <summary>
        ///     Checks whether <paramref name="self"/> is greater than specified value.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is greater than <paramref name="other"/>, otherwise <code>false</code>.</returns>
        public static bool IsGreater<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) == 1;
        
        /// <summary>
        ///     Checks whether <paramref name="self"/> is greater than or equal to specified value.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Value to compare.</param>
        /// <typeparam name="T">Type of comparing values.</typeparam>
        /// <returns><code>true</code>, if <code>this</code> is greater than or equal to <paramref name="other"/>, otherwise <code>false</code>.</returns>
        public static bool IsGreaterOrEqual<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) >= 0;
    }
}