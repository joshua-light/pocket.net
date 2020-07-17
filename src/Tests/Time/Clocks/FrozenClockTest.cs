using Pocket.Time;
using Xunit;

namespace Pocket.Tests.Time.Clocks
{
    public class FrozenClockTest
    {
        [Fact]
        public void Timestamp_ReturnsZeroTimestamp() =>
            Assert.Equal(0, Clock.Watch().Elapsed().TotalMilliseconds);

        private static IClock Clock => Pocket.Time.Clock.Frozen.Now;
    }
}