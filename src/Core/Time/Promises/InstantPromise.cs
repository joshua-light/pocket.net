using System;

namespace Pocket.Common.Time
{
    /// <summary>
    ///     Represents <see cref="IPromise"/>, that is always satisfied.
    /// </summary>
    public sealed class InstantPromise : IPromise
    {
        public void Then(Action @do) => @do();
        public IPromise Then(Func<IPromise> @do) => @do();
    }
}