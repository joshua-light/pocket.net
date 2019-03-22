using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Flows
{
    internal sealed class MappedCollectionFlow<TIn, TOut> : ICollectionFlow<TOut>
    {
        private readonly ICollectionFlow<TIn> _source;
        private readonly Func<TIn, TOut> _map;

        public MappedCollectionFlow(ICollectionFlow<TIn> source, Func<TIn, TOut> map)
        {
            _source = source;
            _map = map;
            
            Added = _source.Added.Select(map);
            Removed = _source.Removed.Select(map);
        }

        public IEnumerable<TOut> Current => _source.Current.Select(_map);
        
        public IFlow<TOut> Added { get; }
        public IFlow<TOut> Removed { get; }
    }
}