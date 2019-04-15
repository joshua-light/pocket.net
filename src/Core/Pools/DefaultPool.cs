using System;
using System.Collections.Generic;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents instance of <see cref="IPool{T}"/> that behaves as simple object pool.
    /// </summary>
    /// <typeparam name="T">Type of pooled items.</typeparam>
    public class DefaultPool<T> : IPool<T> where T : class
    {
        private readonly Func<T> _create;
        private readonly Action<T> _release;
        
        private readonly Stack<T> _items;

        /// <summary>
        ///     Initializes instance of <see cref="DefaultPool{T}"/>.
        /// </summary>
        /// <param name="create">Function that will be used to create new items of pool.</param>
        /// <param name="release">Function that will be used to clear released item state.</param>
        public DefaultPool(Func<T> create, Action<T> release)
        {
            _create = create;
            _release = release;
            
            _items = new Stack<T>();
        }

        /// <inheritdoc cref="IPool{T}"/>.
        public T Item() => _items.Count != 0 ? _items.Pop() : _create();

        /// <inheritdoc cref="IPool{T}"/>.
        public void Release(T item)
        {
            _release(item);
            _items.Push(item);
        }
    }
}