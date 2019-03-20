using System;

namespace Pocket.Common
{
    public static class Disposable
    {
        private sealed class FakeDisposable : IDisposable
        {
            public void Dispose() { }
        }
        
        public static readonly IDisposable Fake = new FakeDisposable();
    }
}