namespace Pocket.Common
{
    /// <summary>
    ///     Represents generic extension-methods for objects of all types.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        ///     Represents either <paramref name="self"/> or <paramref name="default"/> if the first one is <code>null</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="default">Default value that will be used instead of <paramref name="self"/> if it's <code>null</code>.</param>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <returns>Either <paramref name="self"/> or <paramref name="default"/>.</returns>
        public static T Or<T>(this T self, T @default) where T : class =>
            self ?? @default;
    }
}