using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents value that can be initialized without constructor but only once.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    public class Cold<T>
    {
        private T _value;

        /// <summary>
        ///     Internal value that is initialized through <see cref="Freeze"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">There is no value because <see cref="Freeze"/> wasn't called.</exception>
        public T Value
        {
            get
            {
                if (!IsFrozen)
                    throw new InvalidOperationException("Value is not frozen with some concrete value.");

                return _value;
            }
        }
        
        /// <summary>
        ///     Determines whether value was initialized through <see cref="Freeze"/>.
        /// </summary>
        public bool IsFrozen { get; private set; }
        
        /// <summary>
        ///     Initializes internal state with some concrete value.
        /// </summary>
        /// <param name="value">Value that will be an internal state of <see cref="Cold{T}"/>.</param>
        /// <returns>Instance of <code>this</code>.</returns>
        /// <exception cref="InvalidOperationException">Value is already initialized.</exception>
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