namespace Pocket.Common
{
    public static class PoolExtensions
    {
        public static IPool<T> Sync<T>(this IPool<T> self) where T : class =>
            new SyncPool<T>(self);
    }
}