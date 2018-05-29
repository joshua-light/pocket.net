using System;

namespace Pocket.Common
{
    public static class ComparableExtensions
    {
        public static bool IsLess<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) == -1;
        
        public static bool IsLessOrEqual<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) <= 0;
        
        public static bool IsGreater<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) == 1;
        
        public static bool IsGreaterOrEqual<T>(this T self, T other) where T : IComparable<T> =>
            self.CompareTo(other) >= 0;
    }
}