using Xunit;

namespace Pocket.Common.Tests.System.Disposables
{
    public class FakeDisposableTest
    {
        [Fact]
        public void Dispose_DoNothing() =>
            Disposable.Fake.Dispose();
    }
}