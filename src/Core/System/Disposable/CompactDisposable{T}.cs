using System;

namespace Pocket.System
{
    public readonly ref struct CompactDisposable<T>
    {
        private readonly T _state;
        private readonly Action<T> _dispose;

        public CompactDisposable(T state, Action<T> initialize, Action<T> dispose)
        {
            _state = state;
            _dispose = dispose;

            initialize(state);
        }

        public void Dispose() => _dispose(_state);
    }
}