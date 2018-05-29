using System.Collections.Generic;
using Xunit;

namespace Pocket.Common.Tests.Pools.Static
{
    public class ListPoolTest
    {
        [Fact]
        public void Of_ShouldReturnListInstance()
        {
            var list = ListPool.Of<int>();
            Assert.NotNull(list);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(120)]
        public void Of_ShouldReturnEmptyList(int times)
        {
            for (var i = 0; i < times; i++)
                using (var list = ListPool.Of<int>())
                    Assert.Empty(list);
        }

        [Fact]
        public void Dispose_ShouldClearList()
        {
            List<int> numbers;

            using (var list = ListPool.Of<int>())
            {
                list.Add(1);
                list.Add(2);
                list.Add(3);
                
                numbers = list;
            }

            Assert.Empty(numbers);
        }
    }
}