using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for number-types.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        ///     Returns the absolute value of <code>self</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <exception cref="OverflowException"><paramref name="self"/> equals <code>MinValue</code>.</exception>
        /// <returns>Absolute value of <paramref name="self"/>.</returns>
        public static short Abs(this short self) => Math.Abs(self);
        
        /// <summary>
        ///     Returns the absolute value of <code>self</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <exception cref="OverflowException"><paramref name="self"/> equals <code>MinValue</code>.</exception>
        /// <returns>Absolute value of <paramref name="self"/>.</returns>
        public static int Abs(this int self) => Math.Abs(self);
        
        /// <summary>
        ///     Returns the absolute value of <code>self</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <exception cref="OverflowException"><paramref name="self"/> equals <code>MinValue</code>.</exception>
        /// <returns>Absolute value of <paramref name="self"/>.</returns>
        public static long Abs(this long self) => Math.Abs(self);
        
        /// <summary>
        ///     Returns the absolute value of <code>self</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <exception cref="OverflowException"><paramref name="self"/> equals <code>MinValue</code>.</exception>
        /// <returns>Absolute value of <paramref name="self"/>.</returns>
        public static float Abs(this float self) => Math.Abs(self);
        
        /// <summary>
        ///     Returns the absolute value of <code>self</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <exception cref="OverflowException"><paramref name="self"/> equals <code>MinValue</code>.</exception>
        /// <returns>Absolute value of <paramref name="self"/>.</returns>
        public static double Abs(this double self) => Math.Abs(self);
        
        /// <summary>
        ///     Returns the absolute value of <code>self</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <exception cref="OverflowException"><paramref name="self"/> equals <code>MinValue</code>.</exception>
        /// <returns>Absolute value of <paramref name="self"/>.</returns>
        public static decimal Abs(this decimal self) => Math.Abs(self);
    }
}