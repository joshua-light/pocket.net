using System;

namespace Pocket.Common
{
    public static class Progress
    {
        private class FakeOf<T> : IProgress<T>
        {
            public static readonly IProgress<T> Instance = new FakeOf<T>();
            public void Report(T value) { }
        }

        public static IProgress<T> Fake<T>() => FakeOf<T>.Instance;
    }
}