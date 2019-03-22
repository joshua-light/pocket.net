using System;

namespace Pocket.Common.Flows
{
    internal sealed class DistinctFlux<T> : IFlux<T> where T : IEquatable<T>
    {
        private readonly IFlux<T> _flux;

        public DistinctFlux(IFlux<T> flux)
        {
            _flux = flux;
        }

        public T Current => _flux.Current;
        public IDisposable OnNext(Action<T> action) => _flux.OnNext(action);

        public void Pulse(T value)
        {
            if (!_flux.Current.Equals(value))
                _flux.Pulse(value);
        }
    }
}