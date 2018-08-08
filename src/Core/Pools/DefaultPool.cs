using System;
using System.Collections.Generic;

namespace Pocket.Common
{
    public class DefaultPool<T> : IPool<T> where T : class
    {
        private readonly Func<T> _create;
        private readonly Action<T> _release;
        
        private readonly Stack<T> _items;

        public DefaultPool(Func<T> create, Action<T> release)
        {
            _create = create;
            _release = release;
            
            _items = new Stack<T>();
        }

        public T Item() => _items.Count != 0 ? _items.Pop() : _create();

        public void Release(T item)
        {
            _release(item);
            _items.Push(item);
        }
    }
}