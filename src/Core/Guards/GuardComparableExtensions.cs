using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods with invariants checking for types of <see cref="IComparable{T}"/>.
    /// </summary>
    public static class GuardComparableExtensions
    {
        /// <summary>
        ///     Ensures that <paramref name="self"/> is (inclusively) between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Maximum.</param>
        /// <typeparam name="T">Type of <see cref="IComparable{T}"/> value.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is less or greater than specified bounds.</exception>
        public static void EnsureBetween<T>(this T self, T min, T max) where T : IComparable<T>
        {
            if (self.IsLess(min) || self.IsGreater(max))
                throw new ArgumentException($"Specified value {self} must be (inclusively) in range ({min}, {max}).");
        }
        
        /// <summary>
        ///     Ensures that <paramref name="self"/> is less than <paramref name="value"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="value">Value that <paramref name="self"/> will be compared to.</param>
        /// <typeparam name="T">Type of <see cref="IComparable{T}"/> value.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is greater or equal to <paramref name="value"/>.</exception>
        public static void EnsureLess<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsGreaterOrEqual(value))
                throw new ArgumentException($"Specified value {self} must be less than {value}.");
        }
        
        /// <summary>
        ///     Ensures that <paramref name="self"/> is less or equal to <paramref name="value"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="value">Value that <paramref name="self"/> will be compared to.</param>
        /// <typeparam name="T">Type of <see cref="IComparable{T}"/> value.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is greater than <paramref name="value"/>.</exception>
        public static void EnsureLessOrEqual<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsGreater(value))
                throw new ArgumentException($"Specified value {self} must be less or equal to {value}.");
        }

        /// <summary>
        ///     Ensures that <paramref name="self"/> is greater than <paramref name="value"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="value">Value that <paramref name="self"/> will be compared to.</param>
        /// <typeparam name="T">Type of <see cref="IComparable{T}"/> value.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is less or equal to <paramref name="value"/>.</exception>
        public static void EnsureGreater<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsLessOrEqual(value))
                throw new ArgumentException($"Specified value {self} must be greater than {value}.");
        }
        
        /// <summary>
        ///     Ensures that <paramref name="self"/> is greater or equal than <paramref name="value"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="value">Value that <paramref name="self"/> will be compared to.</param>
        /// <typeparam name="T">Type of <see cref="IComparable{T}"/> value.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is less than <paramref name="value"/>.</exception>
        public static void EnsureGreaterOrEqual<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsLess(value))
                throw new ArgumentException($"Specified value {self} must be greater or equal to {value}.");
        }
    }
}