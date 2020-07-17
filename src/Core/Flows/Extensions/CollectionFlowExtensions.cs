using System;
using System.Collections.Generic;

namespace Pocket.Flows
{
    public static class CollectionFlowExtensions
    {
        public static IFlow<int> Count<T>(this ICollectionFlow<T> self) =>
            new CountFlow<T>(self);
        
        public static IFlow<int> Sum<T>(this ICollectionFlow<T> self, Func<T, int> selector) =>
            new SumFlow<int>(self.Select(selector), (x, y) => x + y, (x, y) => x - y);
        
        public static IFlow<double> Sum<T>(this ICollectionFlow<T> self, Func<T, double> selector) =>
            new SumFlow<double>(self.Select(selector), (x, y) => x + y, (x, y) => x - y);

        public static ICollectionFlow<TOut> Select<TIn, TOut>(this ICollectionFlow<TIn> self, Func<TIn, TOut> selector) =>
            new MappedCollectionFlow<TIn, TOut>(self, selector);

        public static ICollectionFlow<T> Where<T>(this ICollectionFlow<T> self, Func<T, bool> predicate) =>
            new FilteredCollectionFlow<T>(self, predicate);
        
        public static ICollectionFlow<T> DispatchedWith<T>(this ICollectionFlow<T> self, Action<Action> dispatcher) =>
            new DispatchedCollectionFlow<T>(self, dispatcher);

        public static ICollectionFlow<T> Merge<T>(this IEnumerable<ICollectionFlow<T>> self) =>
            new MergedCollectionFlow<T>(self);
    }
}