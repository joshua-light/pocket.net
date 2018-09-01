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

        /// <summary>
        ///     Checks whether current instance is letter character.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is letter, otherwise — <code>false</code>.</returns>
        public static bool IsLetter(this char self) => char.IsLetter(self);

        /// <summary>
        ///     Checks whether current instance is letter or digit character.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is  letter or digit, otherwise — <code>false</code>.</returns>
        public static bool IsLetterOrDigit(this char self) => char.IsLetterOrDigit(self);

        /// <summary>
        ///     Checks whether current instance is whitespace character.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is whitespace, otherwise — <code>false</code>.</returns>
        public static bool IsWhiteSpace(this char self) => char.IsWhiteSpace(self);
    }
}