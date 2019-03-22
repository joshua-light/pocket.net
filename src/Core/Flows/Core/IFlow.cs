using System;

namespace Pocket.Common.Flows
{
    /// <summary>
    ///     Represents some value of type <typeparamref name="T"/> that changes over time.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    public interface IFlow<out T>
    {
        /// <summary>
        ///     Subscribes on internal value change.
        /// </summary>
        /// <param name="action">Action that will be called each time internal value of flow changes.</param>
        /// <returns>Instance of <see cref="IDisposable"/>, which <see cref="IDisposable.Dispose"/> should be called for unsubscribe.</returns>
        IDisposable OnNext(Action<T> action);

        /// <summary>
        ///     Value that represents current state of flow.
        /// </summary>
        T Current { get; }
    }
}