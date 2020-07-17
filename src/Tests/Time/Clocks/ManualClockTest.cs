using Pocket.Time;
using Xunit;

namespace Pocket.Tests.Time.Clocks
{
    public class ManualClockTest
    {
        [Fact]
        public void Skip_ShouldChangeElapsedMs()
        {
            var clock = new ManualClock();
            var mark = clock.Watch();

            clock.Skip(ms: 1000);
            
            Assert.Equal(1000, mark.Elapsed().TotalMilliseconds);
        }

        [Fact]
        public void AdditionalSkip_ShouldAddElapsedMs()
        {
            var clock = new ManualClock();
            var mark = clock.Watch();
            
            clock.Skip(ms: 1000);
            clock.Skip(ms: 1000);
            
            Assert.Equal(2000, mark.Elapsed().TotalMilliseconds);
        }
    }
}