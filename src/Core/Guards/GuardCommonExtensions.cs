using System;

namespace Pocket.Common
{
    public static class GuardCommonExtensions
    {
        public static void EnsureNotNull<T>(this T self) where T : class
        {
            if (self == null)
                throw new ArgumentNullException("self", "Specified value must be not null.");
        }
        
        public static void EnsureEqual<T>(this T self, T value) where T : IEquatable<T>
        {
            if (!self.Equals(value))
                throw new ArgumentException($"Specified value {self} must be equal to {value}.");
        }

        public static void EnsureNotEqual<T>(this T self, T value) where T : IEquatable<T>
        {
            if (self.Equals(value))
                throw new ArgumentException($"Specified value {self} must be not equal to {value}.");
        }
    }
}