using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common
{
    public struct Hash
    {
        internal const int Prime = 16777619;
        
        private readonly int _hash;

        public Hash(int hash) =>
            _hash = hash;

        public static Hash Of<T>(T item) =>
            new Hash(item?.GetHashCode() ?? 0);

        public static Hash Of<T>(T[] items) => Of((IList<T>) items);
        public static Hash Of<T>(IList<T> items)
        {
            if (items.Count == 0)
                return Of(0);
            if (items.Count == 1)
                return Of(items[0]);

            var hash = Of(items[0]);

            for (var i = 1; i < items.Count; i++)
                hash = hash.With(items[i]);

            return hash;
        }
        
        public static Hash Of<T>(IEnumerable<T> items) =>
            items.Aggregate(Of(0), (hash, x) => hash.With(x));
        
        public Hash With<T>(T item) =>
            new Hash((_hash * Prime) ^ (item?.GetHashCode() ?? 0));

        public override int GetHashCode() =>
            _hash;
        public static implicit operator int(Hash self) =>
            self.GetHashCode();
    }
}