using NSubstitute;
using Pocket.Common.Tests.Core;
using Xunit;

namespace Pocket.Common.Tests.Pools
{
    public class SyncPoolTest
    {
        [Fact]
        public void AllMethods_ShouldDecorateCorrectly()
        {
            var item = new object();
            var pool = Substitute.For<IPool<object>>();
            pool.Item().Returns(item);

            new Decoration<IPool<object>>(new SyncPool<object>(pool))
                .Call(x => x.Item(), _ => _.Decorates(pool))
                .Call(x => x.Release(item), _ => _.Decorates(pool));
        }
    }
}