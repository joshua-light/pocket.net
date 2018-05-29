using System;

namespace Pocket.Common
{
    public static class TypeExtensions
    {
        public static bool Implements<T>(this Type self) => typeof(T).IsAssignableFrom(self);
        public static bool Extends<T>(this Type self) => typeof(T).IsAssignableFrom(self);
    }
}