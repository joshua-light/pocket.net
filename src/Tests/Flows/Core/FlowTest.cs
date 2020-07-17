using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Core
{
    public class FlowTest
    {
        [Fact]
        public void Empty_ShouldReturnFlowWithDefaultValue() =>
            Assert.Null(Flow.Empty<string>().Current);
    }
}