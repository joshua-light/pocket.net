using System;

namespace Pocket.Common
{
    public static class Pool
    {
        public static IPool<T> Of<T>(Func<T> create, Action<T> release) where T : class =>
            new DefaultPool<T>(create, release);
    }
}