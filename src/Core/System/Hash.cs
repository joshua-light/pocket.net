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

        public static Hash Of<T>(T[] items) => Of((IEnumerable<T>) items);
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