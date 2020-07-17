using System;
using Pocket.Time;
using static Pocket.Guard;

namespace Pocket.Flows
{
    public class DebouncedVoidFlow : IVoidFlow, IDisposable
    {
        private readonly IVoidFlux _flux;
        private readonly IDisposable _subscription;
        private readonly IClock _clock;
        private readonly ISchedule _schedule;
        private readonly TimeSpan _delay;

        private TimeMark _stamp;
        private bool _isScheduled;
        
        public DebouncedVoidFlow(IVoidFlow flow, IClock clock, ISchedule schedule, TimeSpan delay)
        {
            Ensure(flow).NotNull();
            Ensure(schedule).NotNull();
            
            _flux = new PureVoidFlux();

            _clock = clock;
            _schedule = schedule;
            _delay = delay;

            _subscription = flow.OnNext(Debounce);
        }
        
        public void Dispose() => _subscription.Dispose();

        private void Debounce()
        {
            _stamp = _clock.Watch();

            if (_isScheduled)
                return;

            _isScheduled = true;
            _schedule
                .Wait((int) _delay.TotalMilliseconds)
                .Then(() => Pulse());
        }

        private void Pulse()
        {
            var elapsed = _stamp.Elapsed();
            if (elapsed >= _delay)
            {
                _flux.Pulse();
                _isScheduled = false;
            }
            else
            {
                _schedule
                    .Wait((int) elapsed.TotalMilliseconds)
                    .Then(() => Pulse());
            }
        }
        
        public IDisposable OnNext(Action action) => _flux.OnNext(action);
    }
}