namespace Pocket.Common
{
    public class SyncPool<T> : IPool<T> where T : class
    {
        private readonly object _dog = new object();
        private readonly IPool<T> _pool;

        public SyncPool(IPool<T> pool)
        {
            _pool = pool;
        }

        public T Take() { lock (_dog) return _pool.Take(); }
        public void Release(T item) { lock (_dog) _pool.Release(item); }
    }
}