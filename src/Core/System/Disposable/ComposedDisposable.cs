using System;

namespace Pocket
{
    public class ComposedDisposable : IDisposable
    {
        private readonly IDisposable _a;
        private readonly IDisposable _b;
        
        public ComposedDisposable(IDisposable a, IDisposable b) =>
            (_a, _b) = (a, b);

        public void Dispose()
        {
            _a.Dispose();
            _b.Dispose();
        }
    }
}