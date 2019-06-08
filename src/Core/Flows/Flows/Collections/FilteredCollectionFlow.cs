using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Flows
{
    internal sealed class FilteredCollectionFlow<T> : ICollectionFlow<T>
    {
        private readonly IList<T> _source;

        public FilteredCollectionFlow(ICollectionFlow<T> source, Func<T, bool> predicate)
        {
            _source = source.Current.Where(predicate).ToList();

            Added = source.Added.Where(predicate);
            Removed = source.Removed.Where(predicate);

            Added.OnNext(x => _source.Add(x));
            Removed.OnNext(x => _source.Remove(x));
        }

        public IEnumerable<T> Current => _source;
        
        public IFlow<T> Added { get; }
        public IFlow<T> Removed { get; }
    }
}