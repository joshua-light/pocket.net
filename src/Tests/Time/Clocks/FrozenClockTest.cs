using Pocket.Common.Time;
using Xunit;

namespace Pocket.Common.Tests.Time.Clocks
{
    public class FrozenClockTest
    {
        [Fact]
        public void Timestamp_ReturnsZeroTimestamp() =>
            Assert.Equal(0, Clock.Watch().Elapsed().TotalMilliseconds);

        private static IClock Clock => Common.Time.Clock.Frozen.Now;
    }
}