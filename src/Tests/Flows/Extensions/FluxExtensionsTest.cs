using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Extensions
{
    public class FluxExtensionsTest
    {
        [Fact]
        public void Distinct_ShouldReturnDistinctFlux()
        {
            var flux = new PureFlux<int>().Distinct();

            Assert.IsType<DistinctFlux<int>>(flux);
        }
    }
}