using System;
using NSubstitute;
using Xunit;

namespace Pocket.Tests.Pools
{
    public class DefaultPoolTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void Take_ShouldCallCreate_IfNoFreeItems(int times)
        {
            var create = Substitute.For<Func<object>>();
            create.Invoke().Returns(new object());
            var pool = new DefaultPool<object>(create, Substitute.For<Action<object>>());

            for (var i = 0; i < times; i++)
                pool.Item();

            create.Received(times).Invoke();
        }
        
        [Fact]
        public void Take_ShouldReturnItemOfCreateFunc()
        {
            var item = new object();
            var create = Substitute.For<Func<object>>();
            create.Invoke().Returns(item);
            var pool = new DefaultPool<object>(create, Substitute.For<Action<object>>());

            var takenItem = pool.Item();

            Assert.Same(item, takenItem);
        }
        
        [Fact]
        public void Release_ShouldCallReleaseAction()
        {
            var release = Substitute.For<Action<object>>();
            var pool = new DefaultPool<object>(() => new object(), release);

            var item = pool.Item();
            pool.Release(item);

            release.Received(1).Invoke(item);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void Take_ShouldCreateItemOnlyOnce_IfSameItemWasReleased(int times)
        {
            var create = Substitute.For<Func<object>>();
            create.Invoke().Returns(new object());
            var pool = new DefaultPool<object>(create, Substitute.For<Action<object>>());

            for (var i = 0; i < times; i++)
            {
                var item = pool.Item();
                pool.Release(item);
            }
            
            create.Received(1).Invoke();
        }
    }
}