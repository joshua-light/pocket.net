using Xunit;

namespace Pocket.Common.Tests.Monads
{
    public class OneTimeFalseTest
    {
        [Fact]
        public void Value_ShouldBeFalseThenTrue()
        {
            var oneTime = new OneTimeFalse();

            Assert.False(oneTime.Value);
            Assert.True(oneTime.Value);
            Assert.True(oneTime.Value);
            Assert.True(oneTime.Value);
            Assert.True(oneTime.Value);
        }
    }
}