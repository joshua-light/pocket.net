using System;
using System.Collections.Generic;

namespace Pocket.Common
{
    public static class ListPool
    {
        public class PooledList<T> : List<T>, IDisposable
        {
            private readonly IPool<PooledList<T>> _pool;

            public PooledList(IPool<PooledList<T>> pool)
            {
                _pool = pool;
            }

            public void Dispose() => _pool.Release(this);
        }

        private static class ListPoolOf<T>
        {
            private static readonly IPool<PooledList<T>> Pool;

            static ListPoolOf()
            {
                Pool = new DefaultPool<PooledList<T>>(
                        () => new PooledList<T>(Pool),
                        x => x.Clear())
                    .As(x => new SyncPool<PooledList<T>>(x));
            }

            public static PooledList<T> Take() => Pool.Item();
        }

        public static PooledList<T> Of<T>() => ListPoolOf<T>.Take();
    }
}