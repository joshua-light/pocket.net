namespace Pocket
{
    /// <summary>
    ///     Represents instance of <see cref="IPool{T}"/> that supports multithreaded environments.
    /// </summary>
    /// <typeparam name="T">Type of pooled items.</typeparam>
    public class SyncPool<T> : IPool<T> where T : class
    {
        private readonly IPool<T> _pool;

        /// <summary>
        ///     Initializes instance of <see cref="SyncPool{T}"/>.
        /// </summary>
        /// <param name="pool">Instance of <see cref="IPool{T}"/> that will be used as implementation.</param>
        public SyncPool(IPool<T> pool) => _pool = pool;

        /// <inheritdoc cref="IPool{T}"/>.
        public T Item() { lock (_pool) return _pool.Item(); }
        
        /// <inheritdoc cref="IPool{T}"/>.
        public void Release(T item) { lock (_pool) _pool.Release(item); }
    }
}