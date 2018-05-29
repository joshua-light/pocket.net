using System;

namespace Pocket.Common
{
    public class Cold<T>
    {
        private T _value;

        public T Value
        {
            get
            {
                if (!IsFrozen)
                    throw new InvalidOperationException("Value is not frozen with some concrete value.");

                return _value;
            }
        }
        
        public bool IsFrozen { get; private set; }
        
        public Cold<T> Freeze(T value)
        {
            if (IsFrozen)
                throw new InvalidOperationException("Cannot freeze twice.");
            
            _value = value;
            IsFrozen = true;
            return this;
        }
    }
}