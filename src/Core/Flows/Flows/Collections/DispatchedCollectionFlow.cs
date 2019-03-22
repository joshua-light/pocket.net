using System;
using System.Collections.Generic;

namespace Pocket.Common.Flows
{
    internal sealed class DispatchedCollectionFlow<T> : ICollectionFlow<T>
    {
        private readonly ICollectionFlow<T> _collection;

        private readonly IFlow<T> _added;
        private readonly IFlow<T> _removed;

        public DispatchedCollectionFlow(ICollectionFlow<T> collection, Action<Action> dispatcher)
        {
            _collection = collection;

            _added = collection.Added.DispatchedWith(dispatcher);
            _removed = collection.Removed.DispatchedWith(dispatcher);
        }

        public IEnumerable<T> Current => _collection.Current;

        public IFlow<T> Added => _added;
        public IFlow<T> Removed => _removed;
    }
}
