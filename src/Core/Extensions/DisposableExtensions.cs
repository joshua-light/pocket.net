using System;

namespace Pocket.Common
{
    public static class DisposableExtensions
    {
        public static TOut Using<TIn, TOut>(this TIn self, Func<TIn, TOut> map) where TIn : IDisposable
        {
            map.EnsureNotNull();

            return self
                .As(map)
                .Do(_ => self.Dispose());
        }
    }
}