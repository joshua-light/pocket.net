using System;

namespace Pocket.Common
{
    public class OneTime<T> where T : IEquatable<T>
    {
        private readonly T _default;
        private T _current;

        public OneTime(T initial, T @default)
        {
            _default = @default;
            _current = initial;
        }

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
}