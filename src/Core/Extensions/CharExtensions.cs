namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="char"/>.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        ///     Checks whether current instance is digit character.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is digit, otherwise — <code>false</code>.</returns>
        public static bool IsDigit(this char self) => char.IsDigit(self);
    }
}