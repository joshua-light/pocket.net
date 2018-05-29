using Pocket.Common.Numbers;
using Xunit;

namespace Pocket.Common.Tests.Numbers
{
    public class BoundedDoubleTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(20)]
        [InlineData(40)]
        [InlineData(50)]
        public void WithCurrent_ShouldCorrectlyChangeValue(double value)
        {
            var boundedDouble = new BoundedDouble(50, 100);
            
            var newBoundedDouble = boundedDouble.WithCurrent(50 + value);
            
            Assert.Equal(50 + value, newBoundedDouble.Value);
            Assert.Equal(100, newBoundedDouble.Max);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(200)]
        [InlineData(50000000000000)]
        [InlineData(int.MaxValue)]
        public void WithCurrent_ShouldNotExceedMaximum(double value)
        {
            var boundedDouble = new BoundedDouble(50, 100);
            
            var newBoundedDouble = boundedDouble.WithCurrent(50 + value);
            
            Assert.Equal(100, newBoundedDouble.Value);
            Assert.Equal(100, newBoundedDouble.Max);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(200)]
        [InlineData(double.MaxValue)]
        public void WithMax_ShouldReturnNewStruct(double value)
        {
            var boundedDouble = new BoundedDouble(50, 100);
            
            var newBoundedDouble = boundedDouble.WithMax(value);
            
            Assert.NotEqual(boundedDouble, newBoundedDouble);
            Assert.Equal(50, newBoundedDouble.Value);
            Assert.Equal(value, newBoundedDouble.Max);
        }
        
        [Theory]
        [InlineData(49)]
        [InlineData(20)]
        [InlineData(0)]
        public void WithMax_ShouldNotExceedMaximum(double value)
        {
            var boundedDouble = new BoundedDouble(50, 100);
            
            var newBoundedDouble = boundedDouble.WithMax(value);
            
            Assert.Equal(value, newBoundedDouble.Value);
            Assert.Equal(value, newBoundedDouble.Max);
        }
    }
}