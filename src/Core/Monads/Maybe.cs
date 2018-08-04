using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents value that can be either nothing or something.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        /// <summary>
        ///    Instance of nothing.
        /// </summary>
        public static readonly Maybe<T> Nothing = new Maybe<T>();
        
        private readonly T _instance;
        private readonly bool _hasValue;

        internal Maybe(T instance, bool hasValue)
        {
            _instance = instance;
            _hasValue = hasValue;
        }

        /// <summary>
        ///     Determines whether instance of <see cref="Maybe{T}"/> hasn't value.
        /// </summary>
        public bool IsNothing => !_hasValue;
        
        /// <summary>
        ///     Determines whether instance of <see cref="Maybe{T}"/> has value.
        /// </summary>
        public bool HasValue => _hasValue;

        /// <summary>
        ///     Internal value of <see cref="Maybe{T}"/> instance if it exists.
        /// </summary>
        /// <exception cref="InvalidOperationException">Instance represents nothing.</exception>
        public T Value
        {
            get
            {
                if (IsNothing)
                    throw new InvalidOperationException("Cannot access value of nothing.");

                return _instance;
            }
        }

        #region Equals and GetHashCode

        /// <summary>
        ///     Checks whether two instances of <see cref="Maybe{T}"/> are equal.
        /// </summary>
        /// <param name="x">First instance.</param>
        /// <param name="y">Second instance.</param>
        /// <returns><code>true</code> if <paramref name="x"/> is equal to <paramref name="y"/>, otherwise — <code>false</code>.</returns>
        public static bool operator ==(Maybe<T> x, Maybe<T> y) => x.Equals(y);
        
        /// <summary>
        ///     Checks whether two instances of <see cref="Maybe{T}"/> are not equal.
        /// </summary>
        /// <param name="x">First instance.</param>
        /// <param name="y">Second instance.</param>
        /// <returns><code>true</code> if <paramref name="x"/> is not equal to <paramref name="y"/>, otherwise — <code>false</code>.</returns>
        public static bool operator !=(Maybe<T> x, Maybe<T> y) => !x.Equals(y);

        /// <summary>
        ///     Checks whether <code>this</code> object is equal to <paramref name="other"/>.
        /// </summary>
        /// <param name="other">Other instance of <see cref="Maybe{T}"/> to compare with.</param>
        /// <returns><code>true</code> if <code>this</code> is equal to <paramref name="other"/>, otherwise — <code>false</code>.</returns>
        public bool Equals(Maybe<T> other) => Equals(_instance, other._instance);

        /// <summary>
        ///     Checks whether <code>this</code> object is equal to <paramref name="other"/>.
        /// </summary>
        /// <param name="other">Other instance of <see cref="Maybe{T}"/> to compare with.</param>
        /// <returns><code>true</code> if <code>this</code> is equal to <paramref name="other"/>, otherwise — <code>false</code>.</returns>
        public override bool Equals(object other) => other is Maybe<T> maybe && Equals(maybe);
        
        /// <summary>
        ///     Calculates hash code of <see cref="Value"/>.
        /// </summary>
        /// <returns>Hash code of <see cref="Value"/>.</returns>
        public override int GetHashCode() => Value.GetHashCode();

        #endregion
    }
}