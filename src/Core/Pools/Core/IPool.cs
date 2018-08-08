namespace Pocket.Common
{
    /// <summary>
    ///     Represents pool of reusable objects, which can be obtained and released.
    /// </summary>
    /// <typeparam name="T">Type of objects.</typeparam>
    public interface IPool<T> where T : class
    {
        /// <summary>
        ///     Retrieves item from pool.
        /// </summary>
        /// <returns>Instance of item.</returns>
        T Take();
        
        /// <summary>
        ///     Releases item to pool so it can be reused by someone else through <see cref="Take"/>.
        /// </summary>
        /// <param name="item">Instance of item.</param>
        void Release(T item);
    }
}