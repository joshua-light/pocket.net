using System;
using NSubstitute;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class DisposableExtensionsTest
    {
        [Fact]
        public void Using_ShouldCallDispose()
        {
            var disposable = Substitute.For<IDisposable>();
            
            disposable.Using(x => "1");
            
            disposable.Received(1).Dispose();
        }
        
        [Fact]
        public void Using_ShouldCallMapFuncOnDisposable()
        {
            var disposable = Substitute.For<IDisposable>();
            var map = Substitute.For<Func<IDisposable, string>>();
            
            disposable.Using(map);
            
            map.Received(1).Invoke(disposable);
        }
        
        [Fact]
        public void Using_ShouldThrowArgumentNullException_IfMapIsNull() =>
            Assert.Throws<ArgumentNullException>(() => Substitute.For<IDisposable>().Using<IDisposable, int>(null));
    }
}