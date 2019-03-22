using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Flows
{
    internal sealed class FilteredCollectionFlow<T> : ICollectionFlow<T>
    {
        private readonly ICollectionFlow<T> _source;
        private readonly Func<T, bool> _predicate;

        public FilteredCollectionFlow(ICollectionFlow<T> source, Func<T, bool> predicate)
        {
            _source = source;
            _predicate = predicate;

            Added = _source.Added.Where(predicate);
            Removed = _source.Removed.Where(predicate);
        }

        public IEnumerable<T> Current => _source.Current.Where(_predicate);
        
        public IFlow<T> Added { get; }
        public IFlow<T> Removed { get; }
    }
}