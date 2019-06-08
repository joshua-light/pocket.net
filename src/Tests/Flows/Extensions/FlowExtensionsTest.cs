using System;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Extensions
{
    public class FlowExtensionsTest
    {
        [Fact]
        public void PulsedOnNext_ShouldDispatchAndCallOnNext()
        {
            var source = Substitute.For<IFlow<int>>();
            var action = Substitute.For<Action<int>>();

            source.PulsedOnNext(action);

            action.Received(1).Invoke(Arg.Any<int>());
            source.Received(1).OnNext(action);
        }
        
        [Fact]
        public void Select_ShouldReturnMappedFlow()
        {
            var source = Substitute.For<IFlow<int>>();
            var flow = source.Select(x => x.ToString() + "d");

            Assert.IsType<MappedFlow<int, string>>(flow);
        }
        
        [Fact]
        public void DispatchedWith_ShouldReturnDispatchedFlow()
        {    
            var source = Substitute.For<IFlow<int>>();
            var flow = source.Dispatched(Substitute.For<Action<Action>>());

            Assert.IsType<DispatchedFlow<int>>(flow);
        }

        [Fact]
        public void FlowsTo_ShouldCallPulseAfterSubscribe()
        {
            var source = Substitute.For<IFlow<int>>();
            var flux = Substitute.For<IFlux<int>>();
            
            source.FlowsTo(flux);

            flux.Received(1).Pulse(source.Current);
        }

        [Fact]
        public void FlowsTo_ShouldSubscribePulse()
        {
            var flow = new PureFlux<int>();
            var flux = Substitute.For<IFlux<int>>();
            flow.FlowsTo(flux);
            flux.ClearReceivedCalls();

            flow.Pulse(1);
            flux.Received(1).Pulse(1);

            flow.Pulse(2);
            flux.Received(1).Pulse(2);
        }
                
        [Fact]
        public void With_ShouldReturnComposedFlow()
        {
            var source = Substitute.For<IFlow<int>>();
            var flow = source.With(Substitute.For<IFlow<int>>());

            Assert.IsType<ComposedFlow<int, int>>(flow);
        }
    }
}