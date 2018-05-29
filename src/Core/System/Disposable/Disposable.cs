using System;

namespace Pocket.Common
{
    public static class Disposable
    {
        #region Inner Classes

        private sealed class FakeDisposable : IDisposable
        {
            public void Dispose() { }
        }

        #endregion
        
        public static readonly IDisposable Fake = new FakeDisposable();
    }
}