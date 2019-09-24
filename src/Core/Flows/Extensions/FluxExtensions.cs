using System;

namespace Pocket.Common.Flows
{
    public static class FluxExtensions
    {
        public static IFlux<T> Flux<T>(this T self) => new PureFlux<T>(self);
        
        public static IFlux<T> Distinct<T>(this IFlux<T> self) where T : IEquatable<T> =>
            new DistinctFlux<T>(self);

        public static void Increment(this IFlux<int> self) =>
            self.Change(by: +1);
        public static void Decrement(this IFlux<int> self) =>
            self.Change(by: -1);
        public static void Increase(this IFlux<int> self, int by) =>
            self.Change(by);
        public static void Decrease(this IFlux<int> self, int by) =>
            self.Change(-by);
        public static void Change(this IFlux<int> self, int by) =>
            self.Pulse(self.Current + by);
        
        public static void Increment(this IFlux<long> self) =>
            self.Change(by: +1L);
        public static void Decrement(this IFlux<long> self) =>
            self.Change(by: -1L);
        public static void Increase(this IFlux<long> self, long by) =>
            self.Change(by);
        public static void Decrease(this IFlux<long> self, long by) =>
            self.Change(-by);
        public static void Change(this IFlux<long> self, long by) =>
            self.Pulse(self.Current + by);
        
        public static void Increment(this IFlux<float> self) =>
            self.Change(by: +1.0f);
        public static void Decrement(this IFlux<float> self) =>
            self.Change(by: -1.0f);
        public static void Increase(this IFlux<float> self, long by) =>
            self.Change(by);
        public static void Decrease(this IFlux<float> self, long by) =>
            self.Change(-by);
        public static void Change(this IFlux<float> self, float by) =>
            self.Pulse(self.Current + by);
        
        public static void Increment(this IFlux<double> self) =>
            self.Change(by: +1.0);
        public static void Decrement(this IFlux<double> self) =>
            self.Change(by: -1.0);
        public static void Increase(this IFlux<double> self, long by) =>
            self.Change(by);
        public static void Decrease(this IFlux<double> self, long by) =>
            self.Change(-by);
        public static void Change(this IFlux<double> self, double by) =>
            self.Pulse(self.Current + by);
    }
}