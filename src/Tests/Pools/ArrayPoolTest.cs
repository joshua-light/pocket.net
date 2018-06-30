using System;
using System.Collections.Generic;
using Xunit;

namespace Pocket.Common.Tests.Pools
{
    public class ArrayPoolTest
    {
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

        #region Helpers

        private static ArrayPool<T> PoolOf<T>(int size = 1000) => new ArrayPool<T>(size);

        #endregion
    }
}