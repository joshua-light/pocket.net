using System;

namespace Pocket.Common
{
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        public static Maybe<T> Nothing = new Maybe<T>();
        
        private readonly T _instance;
        private readonly bool _hasValue;

        internal Maybe(T instance, bool hasValue)
        {
            _instance = instance;
            _hasValue = hasValue;
        }

        public bool IsNothing => !_hasValue;
        public bool HasValue => _hasValue;

        public T Value
        {
            get
            {
                if (!IsNothing) return _instance;
                
                throw new InvalidOperationException("Cannot access value of nothing.");
            }
        }

        #region Equals and GetHashCode

        public static bool operator ==(Maybe<T> x, Maybe<T> y) => x.Equals(y);
        public static bool operator !=(Maybe<T> x, Maybe<T> y) => !x.Equals(y);

        public bool Equals(Maybe<T> other) => Equals(_instance, other._instance);

        public override bool Equals(object other) => other is Maybe<T> && Equals((Maybe<T>) other);
        public override int GetHashCode() => _instance.GetHashCode();

        #endregion
    }
}