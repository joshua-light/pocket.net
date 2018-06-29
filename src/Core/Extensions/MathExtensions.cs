﻿using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for number-types.
    /// </summary>
    public static class MathExtensions
    {
        #region Abs
        
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
        
        #endregion
        
        #region Or

        /// <summary>
        ///     Represents compact structure that allows fluent calls such as <see cref="IfLess"/> or so,
        ///     which will choose one of two items depending on method semantics.
        /// </summary>
        /// <typeparam name="T">Type of items.</typeparam>
        public struct OrCouple<T> where T : IComparable<T>
        {
            private readonly T _a;
            private readonly T _b;

            /// <summary>
            ///     Initializes instance of <see cref="OrCouple{T}"/>.
            /// </summary>
            /// <param name="a">First item.</param>
            /// <param name="b">Second item.</param>
            public OrCouple(T a, T b)
            {
                _a = a;
                _b = b;
            }

            /// <summary>
            ///     Checks whether first item is less than second and if so returns second.
            /// </summary>
            /// <returns>Maximum of two items.</returns>
            /// <remarks>Works like <see cref="Math.Max"/>.</remarks>
            public T IfLess() => _a.CompareTo(_b) < 0 ? _b : _a;
        }

        /// <summary>
        ///     Allows to compare <code>self</code> with <code>other</code> and choose one of them
        ///     depending on method call on <see cref="OrCouple{T}"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Item to compare.</param>
        /// <returns>Instance of <see cref="OrCouple{T}"/>.</returns>
        public static OrCouple<int> Or(this int self, int other) => new OrCouple<int>(self, other);

        #endregion
    }
}