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
        
        /// <summary>
        ///     Represents number as seconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> seconds.</returns>
        public static TimeSpan Seconds(this int self) => TimeSpan.FromSeconds(self);
        
        /// <summary>
        ///     Represents number as seconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> seconds.</returns>
        public static TimeSpan Seconds(this long self) => TimeSpan.FromSeconds(self);
        
        /// <summary>
        ///     Represents number as seconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> seconds.</returns>
        public static TimeSpan Seconds(this float self) => TimeSpan.FromSeconds(self);
        
        /// <summary>
        ///     Represents number as seconds in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> seconds.</returns>
        public static TimeSpan Seconds(this double self) => TimeSpan.FromSeconds(self);
        
        /// <summary>
        ///     Represents number as minutes in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> minutes.</returns>
        public static TimeSpan Minutes(this int self) => TimeSpan.FromMinutes(self);
        
        /// <summary>
        ///     Represents number as minutes in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> minutes.</returns>
        public static TimeSpan Minutes(this long self) => TimeSpan.FromMinutes(self);
        
        /// <summary>
        ///     Represents number as minutes in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> minutes.</returns>
        public static TimeSpan Minutes(this float self) => TimeSpan.FromMinutes(self);
        
        /// <summary>
        ///     Represents number as minutes in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> minutes.</returns>
        public static TimeSpan Minutes(this double self) => TimeSpan.FromMinutes(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hours(this int self) => TimeSpan.FromHours(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hours(this long self) => TimeSpan.FromHours(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hours(this float self) => TimeSpan.FromHours(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hours(this double self) => TimeSpan.FromHours(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hour(this int self) => TimeSpan.FromHours(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hour(this long self) => TimeSpan.FromHours(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hour(this float self) => TimeSpan.FromHours(self);
        
        /// <summary>
        ///     Represents number as hours in <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Instance of <see cref="TimeSpan"/> with size of <paramref name="self"/> hours.</returns>
        public static TimeSpan Hour(this double self) => TimeSpan.FromHours(self);
    }
}