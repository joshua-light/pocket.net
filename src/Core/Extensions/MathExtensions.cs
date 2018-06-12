using System;

namespace Pocket.Common
{
    public static class DoubleExtensions
    {
        public static short Abs(this short self) => Math.Abs(self);
        public static int Abs(this int self) => Math.Abs(self);
        public static long Abs(this long self) => Math.Abs(self);
        public static float Abs(this float self) => Math.Abs(self);
        public static double Abs(this double self) => Math.Abs(self);
        public static decimal Abs(this decimal self) => Math.Abs(self);
    }
}