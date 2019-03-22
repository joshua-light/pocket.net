using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Flows
{
    public class MergedCollectionFlow<T> : ICollectionFlow<T>
    {
        private readonly IEnumerable<ICollectionFlow<T>> _flows;
        private readonly IFlux<T> _added;
        private readonly IFlux<T> _removed;
        
        public MergedCollectionFlow(IEnumerable<ICollectionFlow<T>> flows)
        {
            _flows = flows;
            _added = new PureFlux<T>();
            _removed = new PureFlux<T>();

            foreach (var flow in flows)
            {
                flow.Added.OnNext(_added.Pulse);
                flow.Removed.OnNext(_removed.Pulse);
            }
        }

        public IEnumerable<T> Current => _flows.SelectMany(x => x.Current);
        public IFlow<T> Added => _added;
        public IFlow<T> Removed => _removed;
    }
}