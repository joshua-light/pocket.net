namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Checks whether <paramref name="self"/> is <code>null</code> or empty string.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is <code>null</code> or empty string, otherwise — <code>false</code>.</returns>
        public static bool IsNullOrEmpty(this string self) => 
            string.IsNullOrEmpty(self);

        /// <summary>
        ///     Returns <paramref name="self"/> if it isn't <code>null</code>, otherwise — <paramref name="@default"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="default">Default value that will be used instead of <paramref name="self"/> if one is null.</param>
        public static string Or(this string self, string @default) =>
            self ?? @default;
    }
}