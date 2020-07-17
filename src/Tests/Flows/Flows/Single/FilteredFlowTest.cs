using System;
using NSubstitute;
using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Flows.Single
{
    public class FilteredFlowTest
    {
        [Fact]
        public void OnNext_ShouldNotBeCalled_IfPredicateIsFalse()
        {
            var action = Substitute.For<Action<int>>();
            var flux = new PureFlux<int>();
            var flow = flux.Where(x => x <= 10);
            flow.OnNext(action);

            flux.Pulse(11);
            flux.Pulse(12);
            flux.Pulse(13);
            flux.Pulse(14);
            
            action.DidNotReceive().Invoke(Arg.Any<int>());
        }

        [Fact]
        public void Current_ShouldReturnLastPulsedValue()
        {
            var action = Substitute.For<Action<int>>();
            var flux = new PureFlux<int>();
            var flow = flux.Where(x => x <= 10);
            flow.OnNext(action);
            
            flux.Pulse(11);
            flux.Pulse(12);
            flux.Pulse(13);
            flux.Pulse(14);
            
            Assert.Equal(14, flow.Current); // TODO: This is actually not correct. There is NO value at all.
        }
    }
}