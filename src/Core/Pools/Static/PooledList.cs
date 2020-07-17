using System;
using System.Collections.Generic;

namespace Pocket
{
    public static class PooledList
    {
        public class One<T> : List<T>, IDisposable
        {
            private readonly IPool<One<T>> _pool;

            public One(IPool<One<T>> pool) =>
                _pool = pool;

            public void Dispose() =>
                _pool.Release(this);
        }

        private static class PoolOf<T>
        {
            private static readonly IPool<One<T>> Pool;

            static PoolOf() =>
                Pool = Pocket.Pool
                    .Of(create: () => new One<T>(Pool),
                        release: x => x.Clear())
                    .Sync();

            public static One<T> Item() => Pool.Item();
        }

        public static One<T> Of<T>() => PoolOf<T>.Item();
    }
}