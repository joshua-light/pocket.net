using System.Collections.Generic;

namespace Pocket.Common.Flows
{
    public static class CollectionFluxExtensions
    {
        public static void Add<T>(this ICollectionFlux<T> self, IEnumerable<T> range) =>
            range.ForEach(x => self.Add(x));
    }
}