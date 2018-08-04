using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Monads
{
    public class OneTimeTest
    {
        [Fact]
        public void Value_ShouldReturnInitialValueAndThenDefault()
        {
            var oneTime = new OneTime<string>("Initial", "Default");

            oneTime.Value.ShouldBe("Initial");
            oneTime.Value.ShouldBe("Default");
            oneTime.Value.ShouldBe("Default");
        }
        
        [Fact]
        public void False_ShouldCreateInstanceThatIsFalseAndThenTrue()
        {
            var oneTime = OneTime.False();

            oneTime.Value.ShouldBeFalse();
            oneTime.Value.ShouldBeTrue();
            oneTime.Value.ShouldBeTrue();
        }
    }
}