using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="TimeSpan"/> (or for converting to it).
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        ///     Represents number as milliseconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> milliseconds.</returns>
        public static TimeSpan Ms(this int self) => TimeSpan.FromMilliseconds(self);
        
        /// <summary>
        ///     Represents number as milliseconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> milliseconds.</returns>
        public static TimeSpan Ms(this long self) => TimeSpan.FromMilliseconds(self);
        
        /// <summary>
        ///     Represents number as milliseconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> milliseconds.</returns>
        public static TimeSpan Ms(this float self) => TimeSpan.FromMilliseconds(self);
        
        /// <summary>
        ///     Represents number as milliseconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> milliseconds.</returns>
        public static TimeSpan Ms(this double self) => TimeSpan.FromMilliseconds(self);
    }
}