using System;

namespace Pocket.Flows
{
    public class ConstFlow<T> : IFlow<T>
    {
        internal static readonly ConstFlow<T> Empty = new ConstFlow<T>();
        
        public ConstFlow(T value = default) =>
            Current = value;
        
        public T Current { get; }
        public IDisposable OnNext(Action<T> action) => Disposable.Fake;
    }
}