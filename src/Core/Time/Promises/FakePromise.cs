using System;

namespace Pocket.Time
{
    /// <summary>
    ///     Represents <see cref="IPromise"/> that will never be satisfied.
    /// </summary>
    public sealed class FakePromise : IPromise
    {
        public void Then(Action @do) { }
        public IPromise Then(Func<IPromise> @do) => this;
    }
}