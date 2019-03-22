using System;

namespace Pocket.Common.Time
{
    /// <summary>
    ///     Represents promise that will be satisfied at some undefined moment of time in future.
    /// </summary>
    public interface IPromise
    {
        /// <summary>
        ///     Continues promise after it's satisfied.
        /// </summary>
        /// <param name="do">Action object, that will be invoked after keeping the promise.</param>
        void Then(Action @do);

        /// <summary>
        ///     Continues promise after it's satisfied piping other <see cref="IPromise"/> from specified continuation.
        /// </summary>
        /// <param name="do">Continuation that generates other <see cref="IPromise"/>.</param>
        /// <returns>Promise object that will be satisfied after <paramref name="do"/>'s generated promise is satisfied.</returns>
        IPromise Then(Func<IPromise> @do);
    }
}