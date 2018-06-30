namespace Pocket.Common
{
    public struct Hash
    {
        internal const int Prime = 16777619;
        
        private readonly int _hash;

        public Hash(int hash)
        {
            _hash = hash;
        }

        public static Hash Of<T>(T item) =>
            item != null ? new Hash(item.GetHashCode()) : new Hash(0);
        
        public Hash With<T>(T item) => new Hash((_hash * Prime) ^ (item != null ? item.GetHashCode() : 0));

        public override int GetHashCode() => _hash;

        public static implicit operator int(Hash self) => self.GetHashCode();
    }
}