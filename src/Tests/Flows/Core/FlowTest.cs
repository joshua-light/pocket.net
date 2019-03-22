using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Core
{
    public class FlowTest
    {
        [Fact]
        public void Empty_ShouldReturnFlowWithDefaultValue() =>
            Assert.Null(Flow.Empty<string>().Current);
    }
}