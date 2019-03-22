using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Flows.Single
{
    public class BufferedFlowTest
    {
        [Fact]
        public void Current_ShouldBeEmpty_IfNoItemsWasPulsed() =>
            Assert.Same(Enumerable.Empty<int>(), new ConstFlow<int>(5).Buffered(Substitute.For<IVoidFlow>()).Current);

        [Fact]
        public void OnNext_ShouldBeCalled_AfterBufferPulse()
        {
            var action = Substitute.For<Action<IEnumerable<int>>>();
            var buffer = new PureVoidFlux();
            var flux = new PureFlux<int>();
            var flow = flux.Buffered(buffer);
            flow.OnNext(action);
            
            buffer.Pulse();
            action.Received(1).Invoke(Arg.Do<IEnumerable<int>>(x => Assert.Equal(new [] { 0 }, x)));
            action.ClearReceivedCalls();

            flux.Pulse(1);
            flux.Pulse(2);
            flux.Pulse(3);
            flux.Pulse(4);
            buffer.Pulse();
            action.Received(1).Invoke(Arg.Do<IEnumerable<int>>(x => Assert.Equal(new [] { 1, 2, 3, 4 }, x)));
        }
    }
}