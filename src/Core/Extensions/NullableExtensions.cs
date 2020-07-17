using System;

namespace Pocket.Extensions
{
    /// <summary>
    ///     Represents generic extension-methods for instances of <see cref="Nullable"/>.
    /// </summary>
    public static class NullableExtensions
    {
        #region Or

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
    }
}