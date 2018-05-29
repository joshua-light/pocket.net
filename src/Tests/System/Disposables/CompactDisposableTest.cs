using System;
using NSubstitute;
using Xunit;

namespace Pocket.Common.Tests.System.Disposables
{
    public class CompactDisposableTest
    {
        [Fact]
        public void Constructor_ShouldCallInitialize()
        {
            var initialize = Substitute.For<Action>();

            new CompactDisposable(initialize, () => { });
            
            initialize.Received(1).Invoke();
        }

        [Fact]
        public void Dispose_ShouldCallDispose()
        {
            var dispose = Substitute.For<Action>();

            using (new CompactDisposable(() => { }, dispose)) { }
            
            dispose.Received(1).Invoke();
        }
    }
}