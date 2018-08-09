using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class TimeSpanExtensionsTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void MillisecondsInt_ShouldCreateTimeSpanWithSpecifiedMilliseconds(int ms) =>
            ms.Milliseconds().TotalMilliseconds.ShouldBe(ms);
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void MillisecondsLong_ShouldCreateTimeSpanWithSpecifiedMilliseconds(long ms) =>
            ms.Milliseconds().TotalMilliseconds.ShouldBe(ms);
        
        [Theory]
        [InlineData(0.0f)]
        [InlineData(1.0f)]
        [InlineData(2.0f)]
        public void MillisecondsFloat_ShouldCreateTimeSpanWithSpecifiedMilliseconds(float ms) =>
            ms.Milliseconds().TotalMilliseconds.ShouldBe(ms);
        
        [Theory]
        [InlineData(0.0)]
        [InlineData(1.0)]
        [InlineData(2.0)]
        public void MillisecondsDouble_ShouldCreateTimeSpanWithSpecifiedMilliseconds(double ms) =>
            ms.Milliseconds().TotalMilliseconds.ShouldBe(ms);
    }
}