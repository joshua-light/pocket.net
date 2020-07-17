using System;
using NSubstitute;
using Pocket.Flows;
using Pocket.Time;
using Xunit;

namespace Pocket.Tests.Flows.Flows.Single.Void
{
    public class DebouncedVoidFlowTest
    {
        [Fact]
        public void Pulse_ShouldNotInvokeAction_IfDelayWasNotElapsed()
        {
            var fixture = new Fixture(delay: 100);
            
            fixture.Pulse();
            fixture.Skip(20);
            
            fixture.Mock.DidNotReceive().Invoke();
        }
        
        [Fact]
        public void Pulse_ShouldInvokeAction_IfDelayWasElapsed()
        {
            var fixture = new Fixture(delay: 100);
            
            fixture.Pulse();
            fixture.Skip(100);
            
            fixture.Mock.Received(1).Invoke();
        }
        
        [Fact]
        public void Pulse_ShouldNotInvokeActionAgain_IfDelayWasNotElapsed()
        {
            var fixture = new Fixture(delay: 100);
            
            fixture.Pulse();
            fixture.Skip(50);
            fixture.Pulse();
            fixture.Skip(50);
            
            fixture.Mock.Received(0).Invoke();
        }
        
        [Fact]
        public void Pulse_ShouldInvokeActionAgain_IfDelayWasElapsed()
        {
            var fixture = new Fixture(delay: 100);
            
            fixture.Pulse();
            fixture.Skip(100);
            fixture.Pulse();
            fixture.Skip(100);
            
            fixture.Mock.Received(2).Invoke();
        }
        
        [Fact]
        public void Pulse_ShouldInvokeAction_IfDelayWasElapsedAfterMultiplePulses()
        {
            var fixture = new Fixture(delay: 100);
            
            fixture.Pulse();
            fixture.Skip(50);
            fixture.Pulse();
            
            fixture.Mock.Received(0).Invoke();
        }
        
        [Fact]
        public void Pulse_ShouldDelayActionInvocation_IfPulsedMultipleTimes()
        {
            var fixture = new Fixture(delay: 100);
            
            fixture.Pulse();
            fixture.Skip(50);
            fixture.Pulse();
            fixture.Skip(23);
            fixture.Pulse();
            fixture.Skip(50);
            fixture.Skip(50);
            
            fixture.Mock.Received(1).Invoke();
        }

        #region Inner Classes

        private class Fixture
        {
            public readonly Action Mock;

            private readonly ManualSchedule _schedule;
            private readonly ManualClock _clock;
            private readonly IVoidFlux _flux;

            public Fixture(int delay)
            {
                Mock = Substitute.For<Action>();
                
                _schedule = new ManualSchedule();
                _clock = new ManualClock();
                _flux = new PureVoidFlux();

                _flux
                    .Debounced(_clock, _schedule, delay.Ms())
                    .OnNext(Mock);
            }

            public void Pulse() => _flux.Pulse();

            public void Skip(int ms)
            {
                _clock.Skip(ms);
                _schedule.Skip(ms);
            }
        }

        #endregion
    }
}