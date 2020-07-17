using System;
using Pocket.Time;

namespace Pocket.Flows
{
    public static class VoidFlowExtensions
    {
        public static IDisposable PulsedOnNext(this IVoidFlow self, Action action)
        {
            action?.Invoke();
            return self.OnNext(action);
        }
        
        public static IVoidFlow Debounced(this IVoidFlow self, TimeSpan delay) =>
            self.Debounced(Clock.UtcNow, new AsyncSchedule(), delay);
        
        public static IVoidFlow Debounced(this IVoidFlow self, IClock clock, TimeSpan delay) =>
            self.Debounced(clock, new AsyncSchedule(), delay);
        
        public static IVoidFlow Debounced(this IVoidFlow self, IClock clock, ISchedule schedule, TimeSpan delay) =>
            new DebouncedVoidFlow(self, clock, schedule, delay);
    }
}