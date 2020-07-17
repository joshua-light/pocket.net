using System;
using NSubstitute;
using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Flows.Single
{
    public class DispatchedFlowTest
    {
        [Fact]
        public void Current_ShouldInitializeWithDefaultValue()
        {
            var fixture = Fixture<int>.New();

            Assert.Equal(0, fixture.Flow.Current);
        }
        
        [Fact]
        public void Current_ShouldReturnCorrectValue_AfterPulse()
        {
            var fixture = Fixture<int>.New();

            fixture.Flux.Pulse(10);
            Assert.Equal(10, fixture.Flow.Current);
        }

        [Fact]
        public void Dispatcher_ShouldBeCalled_OnPulse()
        {
            var fixture = Fixture<int>.New();

            fixture.Flow.OnNext(Substitute.For<Action<int>>());
            fixture.Flux.Pulse(10);
            fixture.Dispatcher.Received(1).Invoke(Arg.Any<Action>());
        }

        [Fact]
        public void OnNext_ShouldBeCalled_AfterDispatch()
        {
            var action = Substitute.For<Action<int>>();
            var flux = new PureFlux<int>();
            var flow = flux.Dispatched(x => x());

            flow.OnNext(action);
            for (var i = 0; i < 10; i++)
                flux.Pulse(10);
            
            action.Received(10).Invoke(10);
        }
        
        #region Helpers
        
        private class Fixture<T>
        {
            public Action<Action> Dispatcher;
            public IFlux<T> Flux;
            public IFlow<T> Flow;

            public static Fixture<T> New()
            {
                var fixture = new Fixture<T>
                {
                    Dispatcher = Substitute.For<Action<Action>>(),
                    Flux = new PureFlux<T>()
                };

                fixture.Flow = new DispatchedFlow<T>(fixture.Flux, fixture.Dispatcher);
                return fixture;
            } 
        }
        
        #endregion
    }
}