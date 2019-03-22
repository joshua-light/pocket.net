namespace Pocket.Common.Flows
{
    /// <summary>
    ///     Represents instance of <see cref="IFlow{T}"/> that can be changed through <see cref="IFlux{T}.Pulse"/>.
    /// </summary>
    /// <remarks>
    ///     Having in mind that flow and flux both mean stream, phonetic difference was used to distinguish them in code.
    ///     Flux sounds like water splash, so it is used to represent changes, while flow's meaning have remained same.
    /// </remarks>
    /// <typeparam name="T">Type of value.</typeparam>
    public interface IFlux<T> : IFlow<T>
    {
        /// <summary>
        ///     Pulses flow with new value notifying all subscribers.
        /// </summary>
        /// <param name="value">New value of flow that will be set to <see cref="IFlow{T}.Current"/>.</param>
        void Pulse(T value);
    }
}