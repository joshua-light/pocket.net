using System;

namespace Pocket.System
{
    public readonly ref struct CompactDisposable
    {
        private readonly Action _dispose;

        public CompactDisposable(Action initialize, Action dispose)
        {
            _dispose = dispose;

            initialize();
        }

        public void Dispose() => _dispose();
    }
}