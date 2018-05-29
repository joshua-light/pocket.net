using Pocket.Common.Numbers;
using Pocket.Common.Numbers.Extensions;
using Xunit;

namespace Pocket.Common.Tests.Numbers.Extensions
{
    public class BoundedDoubleExtensionsTest
    {
        [Fact]
        public void Percent_ShouldCorrectlyCalculateValueAndMaxProportion()
        {
            var boundedDouble = new BoundedDouble(50, 100);
            
            Assert.Equal(50, boundedDouble.Percent());
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(50)]
        public void Increase_ShouldCorrectlyIncreaseCurrent(double value)
        {
            var health = new BoundedDouble(50, 100);

            health = health.Increase(value);
            
            Assert.Equal(50 + value, health.Value);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(50)]
        public void IncreaseMax_ShouldCorrectlyIncreaseMax(double value)
        {
            var health = new BoundedDouble(50, 100);

            health = health.IncreaseMax(value);
            
            Assert.Equal(100 + value, health.Max);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(50)]
        public void Decrease_ShouldCorrectlyDecreaseCurrent(double value)
        {
            var health = new BoundedDouble(50, 100);

            health = health.Decrease(value);
            
            Assert.Equal(50 - value, health.Value);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(50)]
        public void Decrease_ShouldCorrectlyDecreaseMax(double value)
        {
            var health = new BoundedDouble(50, 100);

            health = health.DecreaseMax(value);
            
            Assert.Equal(100 - value, health.Max);
        }
    }
}