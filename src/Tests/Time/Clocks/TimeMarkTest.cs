using System;
using NSubstitute;
using Pocket.Common.Time;
using Xunit;

namespace Pocket.Common.Tests.Time.Clocks
{
    public class TimeMarkTest
    {
        [Fact]
        public void Zero_ShouldReturnZeroTimeSpan() =>
            Assert.Equal(TimeSpan.Zero, TimeMark.Zero.Elapsed());

        [Theory]
        [InlineData(27, 10, 1994)]
        [InlineData(27, 10, 1995)]
        [InlineData(28, 11, 1995)]
        [InlineData(21, 11, 1995)]
        public void Elapsed_ShouldReturnDifferenceBetweenClockTimes(int day, int month, int year)
        {
            var clock = Substitute.For<IClock>();
            var date = new DateTime(year, month, day);
            clock.Time.Returns(date);

            var mark = clock.Watch();
            
            date = date.AddDays(1);
            clock.Time.Returns(date);
            
            Assert.Equal(TimeSpan.FromDays(1), mark.Elapsed());
        }
    }
}