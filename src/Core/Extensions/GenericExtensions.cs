using System;
using System.Collections.Generic;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents generic extension-methods for objects of all types.
    /// </summary>
    public static class GenericExtensions
    {
        #region Or

        /// <summary>
        ///     Represents either <paramref name="self"/> or <paramref name="default"/> if the first one is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="default">Default value that will be used instead of <paramref name="self"/> if it's <code>null</code>.</param>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <returns>Either <paramref name="self"/> or <paramref name="default"/>.</returns>
        public static T Or<T>(this T self, T @default) where T : class =>
            self ?? @default;

        /// <summary>
        ///     Represents either <paramref name="self"/> or <paramref name="default"/> if the first one is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="default">Default value that will be used instead of <paramref name="self"/> if it's <code>null</code>.</param>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <returns>Either <paramref name="self"/> or <paramref name="default"/>.</returns>
        public static T Or<T>(this T? self, T @default) where T : struct =>
            self ?? @default;
        
        /// <summary>
        ///     Represents <paramref name="self"/> as its value or as default of <typeparamref name="T"/> if <paramref name="self"/> is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <returns>Either <paramref name="self"/> inner value or default of <typeparamref name="T"/>.</returns>
        public static T OrDefault<T>(this T? self) where T : struct =>
            self.GetValueOrDefault();

        #endregion
        
        #region Inverted Collections

        public static void AddTo<T>(this T self, IList<T> list) =>
            list.Add(self);

        public static void RemoveFrom<T>(this T self, IList<T> list) =>
            list.Remove(self);

        #endregion
    }
}