using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Flows
{
    public class PureCollectionFlux<T> : ICollectionFlux<T>
    {
        private readonly List<T> _items;
        
        private readonly IFlux<T> _added = new PureFlux<T>();
        private readonly IFlux<T> _removed = new PureFlux<T>();

        public PureCollectionFlux() : this(Enumerable.Empty<T>()) { }
        public PureCollectionFlux(IEnumerable<T> items)
        {
            _items = new List<T>(items);
        }

        public IEnumerable<T> Current => _items;

        public IFlow<T> Added => _added;
        public IFlow<T> Removed => _removed;
        
        public Result Add(T item)
        {
            _items.Add(item);
            _added.Pulse(item);
            
            return Result.Ok();
        }

        public Result Remove(T item)
        {
            var removed = _items.Remove(item);
            if (!removed)
                return Result.Fail($"Cannot remove: item {item} does not exist.");
            
            _removed.Pulse(item);
            return Result.Ok();
        }
    }
}