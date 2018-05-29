using System.Linq;
using Xunit;

namespace Pocket.Common.Tests.Pools.Static
{
    public class ArraySegmentTest
    {
        private readonly int[] _pool = Enumerable.Range(0, 100).ToArray();

        [Theory]
        [InlineData(0, 10)]
        [InlineData(50, 60)]
        [InlineData(80, 99)]
        [InlineData(0, 99)]
        public void Segment_ShouldImplementEnumerableCorrectly(int start, int end)
        {
            var segment = NewSegment(start, end);

            Assert.Equal(Enumerable.Range(start, end - start), segment);
        }

        [Fact]
        public void Indexer_ShouldReturnCorrectValue()
        {
            const int offset = 10;
            const int size = 10;
            var segment = NewSegment(0 + offset, offset + size);

            for (var i = 0; i < size; i++)
                Assert.Equal(i + offset, segment[i]);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(50, 60)]
        [InlineData(80, 100)]
        [InlineData(0, 100)]
        public void Length_ShouldReturnCorrectValue(int start, int end)
        {
            var segment = NewSegment(start, end);

            Assert.Equal(end - start, segment.Length);
        }

        #region Helpers

        private ArrayPool.ArrayPoolOf<int>.PooledArraySegment NewSegment(int start, int end) =>
            new ArrayPool.ArrayPoolOf<int>.PooledArraySegment(_pool, start, end);

        #endregion
    }
}
