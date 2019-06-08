using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Flows
{
    public static class FlowExtensions
    {
        public static IDisposable OnNext<T>(this IFlow<T> self, Action<T, T> action)
        {
            var old = self.Current;

            return self.OnNext(@new =>
            {
                action(old, @new);

                old = @new;
            });
        }
    
        public static IDisposable PulsedOnNext<T>(this IFlow<T> self, Action<T> action)
        {
            action?.Invoke(self.Current);
            return self.OnNext(action);
        }
        
        public static IDisposable FlowsTo<T>(this IFlow<T> self, IFlux<T> flux) =>
            self.PulsedOnNext(flux.Pulse);
        
        public static IFlow<TOut> Select<TIn, TOut>(this IFlow<TIn> self, Func<TIn, TOut> map) =>
            new MappedFlow<TIn, TOut>(self, map);

        public static IFlow<T> Where<T>(this IFlow<T> self, Func<T, bool> predicate) =>
            new FilteredFlow<T>(self, predicate);

        public static IFlow<T> Dispatched<T>(this IFlow<T> self, Action<Action> by) =>
            new DispatchedFlow<T>(self, by);

        public static IFlow<IEnumerable<T>> Buffered<T>(this IFlow<T> self, IVoidFlow buffer) =>
            new BufferedFlow<T>(self, buffer);
        
        public static IFlow<int> FlowSum<T>(this IEnumerable<IFlow<T>> flows, Func<T, int> selector) =>
            new SumFlow<int>(flows.Select(x => x.Select(selector)), (x, y) => x + y);
        
        public static IFlow<double> FlowSum<T>(this IEnumerable<IFlow<T>> flows, Func<T, double> selector) =>
            new SumFlow<double>(flows.Select(x => x.Select(selector)), (x, y) => x + y);
    }
}