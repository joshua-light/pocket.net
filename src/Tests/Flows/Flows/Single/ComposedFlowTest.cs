using System;
using NSubstitute;
using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Flows.Single
{
    public class ComposedFlowTest
    {
        private readonly IFlux<int> _x;
        private readonly IFlux<int> _y;
        private readonly IFlow<(int, int)> _flow;

        public ComposedFlowTest()
        {
            _x = new PureFlux<int>();
            _y = new PureFlux<int>();

            _flow = _x.With(_y);
        }

        [Fact]
        public void Pulse_ShouldNotThrow_IfNothingIsSubscribed()
        {
            _x.Pulse(1);
        }

        [Fact]
        public void OnNext_ShouldBeCalledWithCorrectValues()
        {
            var action = Substitute.For<Action<int, int>>();
            
            _flow.OnNext(action);

            _x.Pulse(1);
            action.Received(1).Invoke(1, 0);

            _y.Pulse(1);
            action.Received(1).Invoke(1, 1);
            
            _x.Pulse(5);
            action.Received(1).Invoke(5, 1);
            
            _y.Pulse(5);
            _y.Pulse(5);
            action.Received(2).Invoke(5, 5);
        }
    }
}