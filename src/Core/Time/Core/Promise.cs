namespace Pocket.Common.Time
{
    /// <summary>
    ///     Represents static type object with helper methods for creation of different types of <see cref="IPromise"/>.
    /// </summary>
    public static class Promise
    {
        /// <summary>
        ///     Fake promise.
        /// </summary>
        public static IPromise Fake = new FakePromise();
        
        /// <summary>
        ///     Instant promise.
        /// </summary>
        public static IPromise Instant = new InstantPromise();
    }
}