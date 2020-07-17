using System;
using NSubstitute;
using Pocket.Extensions;
using Xunit;

namespace Pocket.Tests.Extensions
{
    public class DisposableExtensionsTest
    {
        #region UsingWithFunc
        
        [Fact]
        public void UsingWithFunc_ShouldCallDispose()
        {
            var disposable = Substitute.For<IDisposable>();
            
            disposable.Using(x => "1");
            
            disposable.Received(1).Dispose();
        }
        
        [Fact]
        public void UsingWithFunc_ShouldCallMapFuncOnDisposable()
        {
            var disposable = Substitute.For<IDisposable>();
            var map = Substitute.For<Func<IDisposable, string>>();
            
            disposable.Using(map);
            
            map.Received(1).Invoke(disposable);
        }
        
        [Fact]
        public void UsingWithFunc_ShouldThrowArgumentNullException_IfMapIsNull() =>
            Assert.Throws<ArgumentNullException>(() => Substitute.For<IDisposable>().Using<IDisposable, int>(null));
        
        #endregion

        #region UsingWithAction

        [Fact]
        public void UsingWithAction_ShouldCallDispose()
        {
            var disposable = Substitute.For<IDisposable>();
            
            disposable.Using(x => { });
            
            disposable.Received(1).Dispose();
        }
        
        [Fact]
        public void UsingWithAction_ShouldCallMapFuncOnDisposable()
        {
            var disposable = Substitute.For<IDisposable>();
            var action = Substitute.For<Action<IDisposable>>();
            
            disposable.Using(action);
            
            action.Received(1).Invoke(disposable);
        }
        
        [Fact]
        public void UsingWithAction_ShouldThrowArgumentNullException_IfMapIsNull() =>
            Assert.Throws<ArgumentNullException>(() => Substitute.For<IDisposable>().Using(null));

        #endregion
    }
}