using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pocket.Common.Tests.Pools
{
    public class ArrayPoolTest
    {
        public class SegmentTest
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
        
            [Fact]
            public void Indexer_ShouldThrow_IfIndexIsNegative()
            {
                var segment = Segment(10, 20);

                Assert.Throws<ArgumentException>(() => segment[-1]);
                Assert.Throws<ArgumentException>(() => segment[-1] = 1);
            }

            #region Helpers
        
            private readonly ArrayPool<int> _pool = new ArrayPool<int>(100);

            private ArrayPool<int>.Segment Segment(int start, int end) =>
                new ArrayPool<int>.Segment(_pool, start, end);

            #endregion
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(200)]
        public void Take_ShouldReturnArrayWithSpecifiedSize(int size)
        {
            var pool = PoolOf<int>();
            
            var array = pool.Take(size);
            
            Assert.Equal(size, array.Length);
        }

        [Fact]
        public void Take_ShouldThrowException_IfThereIsNoEnoughMemoryToAllocate()
        {
            var pool = PoolOf<int>();
            var segments = new List<ArrayPool<int>.Segment>();
            for (var i = 0; i < 100; i++)
                segments.Add(pool.Take(10));

            Assert.Throws<InvalidOperationException>(() => segments.Add(pool.Take(10)));
        }

        [Fact]
        public void Take_ShouldReturnClearSegment()
        {
            var pool = PoolOf<int>(1);
            
            var segment = pool.Take(1);
            segment[0] = 123;
            segment.Dispose();

            segment = pool.Take(1);
            
            Assert.NotEqual(123, segment[0]);
        }

        [Fact]
        public void Take_ShouldCheckFreeSpaceAtStartOfBuffer()
        {
            var pool = PoolOf<int>(2);

            var a = pool.Take(1);
            var b = pool.Take(1);
            
            a.Dispose();

            // This should not throw exception.
            pool.Take(1);
        }
        
        [Fact]
        public void Take_ShouldCheckFreeSpaceInBetweenOfSegments()
        {
            var pool = PoolOf<int>(3);

            var a = pool.Take(1);
            var b = pool.Take(1);
            var c = pool.Take(1);
            
            b.Dispose();

            // This should not throw exception.
            pool.Take(1);
        }

        #region Helpers

        private static ArrayPool<T> PoolOf<T>(int size = 1000) => new ArrayPool<T>(size);

        #endregion
    }
}