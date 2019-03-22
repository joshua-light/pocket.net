using System;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Fluxes.Single
{
    public class PureFluxTest
    {
        public class VoidFlux
        {
            private readonly IVoidFlux _flux = new PureVoidFlux();

            [Fact]
            public void OnNext_ShouldBeCalledOnPulse()
            {
                var action = Substitute.For<Action>();

                _flux.OnNext(action);
                _flux.Pulse();
                _flux.Pulse();

                action.Received(2).Invoke();
            }

            [Fact]
            public void OnNext_ShouldThrowGuardException_IfActionIsNull() =>
                Assert.Throws<ArgumentNullException>(() => _flux.OnNext(null));

            [Fact]
            public void Pulse_ShouldNotThrow_IfOnNextWasNotCalled() => new PureVoidFlux().Pulse();
        }

        public class IntFlux
        {
            private readonly IFlux<int> _flux = new PureFlux<int>(10);

            [Fact]
            public void Current_ShouldReturnValueFromConstructor()
            {
                Assert.Equal(10, _flux.Current);
            }
            
            [Fact]
            public void Current_ShouldReturnNewValueAfterPulse()
            {
                _flux.Pulse(15);

                Assert.Equal(15, _flux.Current);
            }

            [Fact]
            public void OnNext_ShouldBeCalledOnPulse()
            {
                var action = Substitute.For<Action<int>>();

                _flux.OnNext(action);
                _flux.Pulse(10);
                _flux.Pulse(11);

                action.Received(1).Invoke(10);
                action.Received(1).Invoke(11);
            }
        }
    }
}