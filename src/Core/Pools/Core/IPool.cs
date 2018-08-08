namespace Pocket.Common
{
    /// <summary>
    ///     Represents pool of reusable objects, which can be obtained and released.
    /// </summary>
    /// <typeparam name="T">Type of pooled items.</typeparam>
    public interface IPool<T> where T : class
    {
        /// <summary>
        ///     Retrieves item from pool.
        /// </summary>
        /// <returns>Instance of item.</returns>
        T Item();
        
        /// <summary>
        ///     Releases item to pool so it can be reused by someone else through <see cref="Item"/>.
        /// </summary>
        /// <param name="item">Instance of item.</param>
        void Release(T item);
    }
}