using System;
using NSubstitute;
using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Fluxes.Single
{
    public class DistinctFluxTest
    {
        [Fact]
        public void Current_ShouldCallInner()
        {
            var flux = Substitute.For<IFlux<int>>();
            var sut = new DistinctFlux<int>(flux);

            var a = sut.Current;
            var b = flux.Received(1).Current;
        }
        
        [Fact]
        public void OnNext_ShouldCallInner()
        {
            var action = Substitute.For<Action<int>>();
            var flux = Substitute.For<IFlux<int>>();
            var sut = new DistinctFlux<int>(flux);

            sut.OnNext(action);

            flux.Received(1).OnNext(action);
        }

        [Fact]
        public void Pulse_ShouldCallInner()
        {
            var flux = Substitute.For<IFlux<int>>();
            var sut = new DistinctFlux<int>(flux);

            sut.Pulse(10);

            flux.Received(1).Pulse(10);
        }
        
        [Fact]
        public void OnNext_ShouldNotBeCalled_IfValueIsEqualToCurrent()
        {
            var action = Substitute.For<Action<int>>();
            var flux = new DistinctFlux<int>(new PureFlux<int>());
            flux.OnNext(action);

            flux.Pulse(10);
            flux.Pulse(10);
            flux.Pulse(11);
            flux.Pulse(11);

            action.Received(1).Invoke(10);
            action.Received(1).Invoke(11);
        }
    }
}