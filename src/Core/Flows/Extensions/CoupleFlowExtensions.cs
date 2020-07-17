using System;

namespace Pocket.Flows
{
    public static class CoupleFlowExtensions
    {
        public static IFlow<(T1, T2)> With<T1, T2>(this IFlow<T1> self, IFlow<T2> other) =>
            new ComposedFlow<T1, T2>(self, other);

        public static IDisposable OnNext<T1, T2>(this IFlow<(T1, T2)> self, Action<T1, T2> action) =>
            self.OnNext(x => action(x.Item1, x.Item2));
        
        public static IDisposable PulsedOnNext<T1, T2>(this IFlow<(T1, T2)> self, Action<T1, T2> action)
        {
            var (a, b) = self.Current;
            
            action?.Invoke(a, b);

            return self.OnNext(action);
        }
    }
}