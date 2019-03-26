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

        public static IDisposable Of(Action begin, Action end) =>
            new CompactDisposable(begin, end);

        public static IDisposable Of<T>(T x, Action<T> begin, Action<T> end) =>
            new CompactDisposable(() => begin(x), () => end(x));
    }
}