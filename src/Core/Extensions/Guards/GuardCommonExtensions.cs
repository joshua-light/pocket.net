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
        ///     Ensures that some fact (represented by <paramref name="predicate"/>) about object is <code>true</code>.
        ///     Otherwise throws.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="predicate">Predicate about object.</param>
        /// <typeparam name="T">Type of <code>this</code> object.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException"><paramref name="predicate"/> returns <code>false</code>.</exception>
        public static void Ensure<T>([NoEnumeration] this T self, Func<T, bool> predicate) =>
            self.Ensure(predicate, "Specified predicate didn't match.");
        
        /// <summary>
        ///     Ensures that some fact (represented by <paramref name="predicate"/>) about object is <code>true</code>.
        ///     Otherwise throws.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="predicate">Predicate about object.</param>
        /// <param name="message">Error message that will be used in case, when <paramref name="predicate"/> is <code>false</code>.</param>
        /// <typeparam name="T">Type of <code>this</code> object.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException"><paramref name="predicate"/> returns <code>false</code>.</exception>
        public static void Ensure<T>([NoEnumeration] this T self, Func<T, bool> predicate, string message)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));
            
            predicate.EnsureNotNull();
            
            if (!predicate(self))
                throw new ArgumentException(message);
        }

        /// <summary>
        ///     Throws if <paramref name="self"/> is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        public static void EnsureNotNull<T>([NoEnumeration] this T self) where T : class =>
            self.EnsureNotNull("Specified value must be not null.");
        
        /// <summary>
        ///     Throws if <paramref name="self"/> is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="message">Message that describes why <paramref name="self"/> should be not <code>null</code>.</param>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="self"/> is <code>null</code>.</exception>
        public static void EnsureNotNull<T>([NoEnumeration] this T self, string message) where T : class
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self), message);
        }
        
        /// <summary>
        ///     Throws if <paramref name="self"/> is not <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is not <code>null</code>.</exception>
        public static void EnsureNull<T>([NoEnumeration] this T self) where T : class =>
            self.EnsureNull("Specified value must be null.");
        
        /// <summary>
        ///     Throws if <paramref name="self"/> is not <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="message">Message that describes why <paramref name="self"/> should be <code>null</code>.</param>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is not <code>null</code>.</exception>
        public static void EnsureNull<T>([NoEnumeration] this T self, string message) where T : class
        {
            if (self == null)
                throw new ArgumentException(nameof(self), message);
        }

        /// <summary>
        ///     Throws if <paramref name="self"/> is not of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type that object must be of.</typeparam>
        /// <exception cref="ArgumentException"><paramref name="self"/> is not of type <typeparamref name="T"/>.</exception>
        public static void EnsureIs<T>([NoEnumeration] this object self)
        {
            self.EnsureNotNull();
            
            if (self.GetType() != typeof(T))
                throw new ArgumentException($"Specified object is not {typeof(T).Name}.");
        }
    }
}