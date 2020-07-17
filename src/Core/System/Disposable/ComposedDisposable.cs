using System;

namespace Pocket.System
{
    public readonly ref struct ComposedDisposable
    {
        private readonly IDisposable _a;
        private readonly IDisposable _b;
        
        public ComposedDisposable(IDisposable a, IDisposable b) =>
            (_a, _b) = (a, b);

        public void Dispose()
        {
            _b.Dispose();
            _a.Dispose();
        }
    }
}