using System;
using Pocket.Common.Time;
using Xunit;

namespace Pocket.Common.Tests.Time.Temporals
{
    public class TemporalGateTest
    {
        [Theory]
        [InlineData(10)]
        [InlineData(1000)]
        [InlineData(1000000)]
        [InlineData(10000000)]
        public void Open_ShouldBeTrue_IfElapsedEnoughTime(int ms)
        {
            var gate = new TemporalGate(ms);

            gate.Exist(TimeSpan.FromMilliseconds(ms * 2));

            Assert.True(gate.Open());
        }
        
        [Fact]
        public void Open_ShouldBeAlwaysTrue_IfIntervalIsZero()
        {
            var gate = new TemporalGate(0);

            Assert.True(gate.Open());
            Assert.True(gate.Open());
            Assert.True(gate.Open());
            Assert.True(gate.Open());
            Assert.True(gate.Open());
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1000)]
        [InlineData(1000000)]
        [InlineData(10000000)]
        public void Open_ShouldBeFalse_IfElapsedNotEnoughTime(int ms)
        {
            var gate = new TemporalGate(ms);
            gate.Exist(TimeSpan.FromMilliseconds(ms - 1));

            Assert.False(gate.Open());
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1000)]
        [InlineData(1000000)]
        [InlineData(10000000)]
        public void Open_ShouldBeTrue_AndThenFalse_IfElapsedEnoughTime(int ms)
        {
            var gate = new TemporalGate(ms);
            gate.Exist(TimeSpan.FromMilliseconds(ms));
            
            Assert.True(gate.Open());
            Assert.False(gate.Open());
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1000)]
        [InlineData(1000000)]
        [InlineData(10000000)]
        public void Exist_ShouldAccumulateElapsedTime(int ms)
        {
            var gate = new TemporalGate(ms);
            
            gate.Exist(TimeSpan.FromMilliseconds(ms - 1));
            Assert.False(gate.Open());

            gate.Exist(TimeSpan.FromMilliseconds(1));
            Assert.True(gate.Open());
        }
    }
}