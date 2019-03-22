using System;

namespace Pocket.Common.Flows
{
    public sealed class PureFlux<T> : IFlux<T>
    {
        private event Action<T> Next;
        private T _current;
        
        public PureFlux(T current = default(T))
        {
            _current = current;
        }

        public T Current => _current;
        
        public IDisposable OnNext(Action<T> action) =>
            new CompactDisposable(
                () => Next += action,
                () => Next -= action);

        public void Pulse(T value)
        {
            _current = value;
            Next?.Invoke(value);
        }
    }
}