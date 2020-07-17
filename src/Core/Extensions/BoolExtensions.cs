namespace Pocket
{
    /// <summary>
    ///     Represents extension-methods for <see cref="bool"/>.
    /// </summary>
    public static class BoolExtensions
    {
        /// <summary>
        ///     Applies logical <code>&&</code> operator for both <paramref name="self"/> and <paramref name="other"/> values.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Second operand of logical operation.</param>
        /// <returns>Result of <code>&&</code> operator aplied to <paramref name="self"/> and <paramref name="other"/>.</returns>
        public static bool And(this bool self, bool other) => self && other;
        
        /// <summary>
        ///     Applies logical <code>||</code> operator for both <paramref name="self"/> and <paramref name="other"/> values.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Second operand of logical operation.</param>
        /// <returns><paramref name="self"/> || <paramref name="other"/>.</returns>
        public static bool Or(this bool self, bool other) => self || other;

        /// <summary>
        ///     Applies logical implication operator for both <paramref name="self"/> and <paramref name="other"/> values.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Second operand of logical operation.</param>
        /// <returns>If <paramref name="self"/> is <code>true</code>, then <paramref name="other"/>, otherwise <code>true</code>.</returns>
        public static bool Implication(this bool self, bool other) => !self || other;
    }
}