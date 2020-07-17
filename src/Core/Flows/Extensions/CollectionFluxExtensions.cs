using System.Collections.Generic;

namespace Pocket.Flows
{
    public static class CollectionFluxExtensions
    {
        public static void Add<T>(this ICollectionFlux<T> self, IEnumerable<T> range) =>
            range.ForEach(x => self.Add(x));
    }
}