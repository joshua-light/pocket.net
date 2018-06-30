using System.Linq;
using Xunit;

namespace Pocket.Common.Tests.Pools
{
    public class ArraySegmentTest
    {
        [Theory]
        [InlineData(0, 10)]
        [InlineData(50, 60)]
        [InlineData(80, 100)]
        [InlineData(0, 100)]
        public void Length_ShouldReturnCorrectValue(int start, int end)
        {
            var segment = Segment(start, end);

            Assert.Equal(end - start, segment.Length);
        }
        
        [Theory]
        [InlineData(0, 10)]
        [InlineData(50, 60)]
        [InlineData(80, 99)]
        [InlineData(0, 99)]
        public void Segment_ShouldImplementEnumerable(int start, int end)
        {
            var segment = Segment(start, end);

            for (var i = 0; i < end - start; i++)
                segment[i] = start + i;

            Assert.Equal(Enumerable.Range(start, end - start), segment);
        }

        #region Helpers
        
        private readonly ArrayPool<int> _pool = new ArrayPool<int>(100);

        private ArrayPool<int>.Segment Segment(int start, int end) =>
            new ArrayPool<int>.Segment(_pool, start, end);

        #endregion
    }
}
