using System;

namespace Pocket.Common.Flows
{
    public static class FluxExtensions
    {
        public static IFlux<T> Distinct<T>(this IFlux<T> self) where T : IEquatable<T> =>
            new DistinctFlux<T>(self);
    }
}