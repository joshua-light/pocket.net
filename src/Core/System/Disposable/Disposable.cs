using System;

namespace Pocket.System
{
    public static class Disposable
    {
        private sealed class FakeDisposable : IDisposable
        {
            public void Dispose() { }
        }
        
        public static readonly IDisposable Fake = new FakeDisposable();

        public static CompactDisposable Of(Action begin, Action end) =>
            new CompactDisposable(begin, end);

        public static CompactDisposable<T> Of<T>(T x, Action<T> begin, Action<T> end) =>
            new CompactDisposable<T>(x, begin, end);
        
        public static ComposedDisposable With(this IDisposable self, IDisposable other) =>
            new ComposedDisposable(self, other);
    }
}