using System;

namespace Pocket
{
    /// <summary>
    ///     Represents static methods related to enum of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of enum.</typeparam>
    public static class EnumOf<T> where T : struct
    {
        /// <summary>
        ///     Array of enum values.
        /// </summary>
        public static readonly T[] Values = (T[]) Enum.GetValues(typeof(T));

        /// <summary>
        ///     Converts string to enum value.
        /// </summary>
        /// <param name="text">String representation of value.</param>
        /// <returns>Converted value.</returns>
        public static T Parse(string text) => (T) Enum.Parse(typeof(T), text);
        
        /// <summary>
        ///     Converts string to enum value in safe way.
        /// </summary>
        /// <param name="text">String representation of value.</param>
        /// <param name="value">Converted value.</param>
        /// <returns><code>true</code> if <paramref name="text"/> represented correct value of <typeparamref name="T"/>, otherwise — <code>false</code>.</returns>
        public static bool TryParse(string text, out T value) => Enum.TryParse(text, out value);
    }
}