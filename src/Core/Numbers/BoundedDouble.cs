using System;

namespace Pocket.Common.Numbers
{
    public struct BoundedDouble
    {
        public BoundedDouble(double value) : this(value, value) { }
        public BoundedDouble(double value, double max)
        {
            Max = Math.Max(0, max);
            Value = Math.Max(0, Math.Min(value, Max));
        }

        public double Value { get; }
        public double Max { get; }

        public BoundedDouble WithCurrent(double value) => new BoundedDouble(value, Max);
        public BoundedDouble WithMax(double max) => new BoundedDouble(Value, max);
    }
}