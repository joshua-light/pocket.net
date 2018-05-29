using System;

namespace Pocket.Common
{
    public static class GuardComparableExtensions
    {
        public static void EnsureBetween<T>(this T self, T min, T max) where T : IComparable<T>
        {
            if (self.IsLess(min) || self.IsGreater(max))
                throw new ArgumentException($"Specified value {self} must be in range ({min}, {max}).");
        }

        public static void EnsureLess<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsGreaterOrEqual(value))
                throw new ArgumentException($"Specified value {self} must be less than {value}.");
        }
        
        public static void EnsureLessOrEqual<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsGreater(value))
                throw new ArgumentException($"Specified value {self} must be less or equal to {value}.");
        }

        public static void EnsureGreater<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsLessOrEqual(value))
                throw new ArgumentException($"Specified value {self} must be greater than {value}.");
        }

        public static void EnsureGreaterOrEqual<T>(this T self, T value) where T : IComparable<T>
        {
            if (self.IsLess(value))
                throw new ArgumentException($"Specified value {self} must be greater or equal to {value}.");
        }
    }
}