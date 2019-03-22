using System;
using System.Linq;

namespace Pocket.Common.Flows
{
    internal class CountFlow<T> : IFlow<int>
    {
        private readonly IFlux<int> _flux;

        public CountFlow(ICollectionFlow<T> collection)
        {
            _flux = new PureFlux<int>(collection.Current.Count());

            collection.Added.OnNext(_ => _flux.Pulse(_flux.Current + 1));
            collection.Removed.OnNext(_ => _flux.Pulse(_flux.Current - 1));
        }

        public int Current => _flux.Current;
        
        public IDisposable OnNext(Action<int> action) => _flux.OnNext(action);
    }
}