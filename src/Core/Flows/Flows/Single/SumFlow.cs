using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Flows
{
    internal class SumFlow<T> : IFlow<T>
    {
        private readonly IFlux<T> _flux;
        
        public SumFlow(IEnumerable<IFlow<T>> flows, Func<T, T, T> add)
        {
            _flux = new PureFlux<T>(flows.Aggregate(default(T), (current, x) => add(current, x.Current)));

            foreach (var flow in flows)
                flow.OnNext(_ => _flux.Pulse(flows.Aggregate(default(T), (current, x) => add(current, x.Current))));
        }
        
        public SumFlow(ICollectionFlow<T> collection, Func<T, T, T> add, Func<T, T, T> subtract)
        {
            _flux = new PureFlux<T>();
            
            if (collection.Current.Any())
                _flux.Pulse(collection.Current.Aggregate(add));

            collection.Added.OnNext(x => _flux.Pulse(add(_flux.Current, x)));
            collection.Removed.OnNext(x => _flux.Pulse(subtract(_flux.Current, x)));
        }

        public T Current => _flux.Current;
        public IDisposable OnNext(Action<T> action) => _flux.OnNext(action);
    }
}