using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents value that has some initial state for one read and then resets to default.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OneTime<T> where T : IEquatable<T>
    {
        private readonly T _default;
        private T _current;

        /// <summary>
        ///     Initializes instance of <see cref="OneTime{T}"/>.
        /// </summary>
        /// <param name="initial">Initial value that will be represented by <see cref="Value"/> for one read.</param>
        /// <param name="default">Default value.</param>
        public OneTime(T initial, T @default)
        {
            _default = @default;
            _current = initial;
        }

        /// <summary>
        ///     For first time returns initial value, then always returns default value.
        /// </summary>
        public T Value
        {
            get
            {
                var current = _current;
                if (current.Equals(_default))
                    return current;

                _current = _default;
                return current;
            }
        }
    }

    /// <summary>
    ///     Represents static methods for creating various predefined instances of <see cref="OneTime{T}"/>.
    /// </summary>
    public static class OneTime
    {
        /// <summary>
        ///     Creates instance of <see cref="OneTime{T}"/>
        ///     where initial value will be <code>false</code> and default — <code>true</code>.
        /// </summary>
        /// <returns>New instance of <see cref="OneTime{T}"/>.</returns>
        public static OneTime<bool> False() => new OneTime<bool>(initial: false, @default: true);
    }
}