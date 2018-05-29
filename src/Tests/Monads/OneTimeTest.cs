using Xunit;

namespace Pocket.Common.Tests.Monads
{
    public class OneTimeTest
    {
        [Fact]
        public void Value_ShouldReturnInitialValueAndThenDefault()
        {
            var oneTime = new OneTime<string>("Initial", "Default");

            Assert.Equal("Initial", oneTime.Value);
            Assert.Equal("Default", oneTime.Value);
            Assert.Equal("Default", oneTime.Value);
            Assert.Equal("Default", oneTime.Value);
            Assert.Equal("Default", oneTime.Value);
        }
    }
}