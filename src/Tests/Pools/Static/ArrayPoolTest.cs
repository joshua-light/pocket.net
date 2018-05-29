using System;
using System.Collections.Generic;
using Xunit;

namespace Pocket.Common.Tests.Pools.Static
{
    public class ArrayPoolTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(200)]
        public void Of_ShouldReturnArrayWithSpecifiedSize(int size)
        {
            var array = ArrayPool.Of<int>(size);
            Assert.Equal(size, array.Length);
        }

        [Fact]
        public void Of_ShouldThrowException_IfThereIsNoEnoughMemoryToAllocate()
        {
            var segments = new List<ArrayPool.ArrayPoolOf<double>.PooledArraySegment>();
            for (var i = 0; i < 100; i++)
                segments.Add(ArrayPool.Of<double>(10));

            Assert.Throws<InvalidOperationException>(() => segments.Add(ArrayPool.Of<double>(10)));

            segments.ForEach(x => x.Dispose());
        }

        [Fact]
        public void Of_ShouldReleaseFirstElement()
        {
            const int oneThirdSize = ArrayPool.ArrayPoolOf<double>.Size / 3;
            var first = ArrayPool.Of<double>(oneThirdSize);
            var second = ArrayPool.Of<double>(oneThirdSize);
            var third = ArrayPool.Of<double>(oneThirdSize);

            first.Dispose();
            var fourth = ArrayPool.Of<double>(oneThirdSize);

            second.Dispose();
            third.Dispose();
            fourth.Dispose();
        }

        [Fact]
        public void Of_ShouldReleaseSecondElement()
        {
            const int oneThirdSize = ArrayPool.ArrayPoolOf<double>.Size / 3;
            var first = ArrayPool.Of<double>(oneThirdSize);
            var second = ArrayPool.Of<double>(oneThirdSize);
            var third = ArrayPool.Of<double>(oneThirdSize);

            second.Dispose();
            var fourth = ArrayPool.Of<double>(oneThirdSize);

            first.Dispose();
            third.Dispose();
            fourth.Dispose();
        }

        [Fact]
        public void Of_ShouldCorrectlyReleaseSegments()
        {
            const int oneFourthSize = ArrayPool.ArrayPoolOf<double>.Size / 4;
            var first = ArrayPool.Of<double>(oneFourthSize);
            var second = ArrayPool.Of<double>(oneFourthSize);
            var third = ArrayPool.Of<double>(oneFourthSize);
            var fourth = ArrayPool.Of<double>(oneFourthSize);

            third.Dispose();
            fourth.Dispose();
            third = ArrayPool.Of<double>(oneFourthSize);
            fourth = ArrayPool.Of<double>(oneFourthSize);

            first.Dispose();
            second.Dispose();
            third.Dispose();
            fourth.Dispose();
        }

        [Fact]
        public void Of_ShouldReturnClearSegment()
        {
            var segment = ArrayPool.Of<string>(1);
            segment[0] = "Test";
            segment.Dispose();

            segment = ArrayPool.Of<string>(1);
            Assert.NotEqual("Test", segment[0]);
        }
    }
}