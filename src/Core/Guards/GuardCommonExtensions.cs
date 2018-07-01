using System;
using JetBrains.Annotations;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods with invariants checking for generic types.
    /// </summary>
    public static class GuardCommonExtensions
    {
        /// <summary>
        ///     Throws if <paramref name="self"/> is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        public static void EnsureNotNull<T>([NoEnumeration] this T self) where T : class
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self), "Specified value must be not null.");
        }
        
        /// <summary>
        ///     Throws if <paramref name="self"/> is not equal to <paramref name="value"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="value">Object to compare with.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is not equal to <paramref name="value"/>.</exception>
        public static void EnsureEqual<T>(this T self, T value) where T : IEquatable<T>
        {
            if (!self.Equals(value))
                throw new ArgumentException($"Specified value {self} must be equal to {value}.");
        }

        /// <summary>
        ///     Throws if <paramref name="self"/> is equal to <paramref name="value"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="value">Object to compare with.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is equal to <paramref name="value"/>.</exception>
        public static void EnsureNotEqual<T>(this T self, T value) where T : IEquatable<T>
        {
            if (self.Equals(value))
                throw new ArgumentException($"Specified value {self} must be not equal to {value}.");
        }
    }
}