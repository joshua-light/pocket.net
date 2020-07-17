using System;

namespace Pocket.System
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
        ///     Converts string to enum value in safe way.
        /// </summary>
        /// <param name="text">String representation of value.</param>
        /// <returns>
        ///     Enum value if <paramref name="text"/> represented correct value of <typeparamref name="T"/>,
        ///     <code>null</code> otherwise.
        /// </returns>
        public static T? From(string text) =>
            Enum.TryParse<T>(text, out var value) ? value : (T?) null;
    }
}