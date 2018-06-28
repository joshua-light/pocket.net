using System;
using System.Reflection;

namespace Pocket.Common
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type self)
        {
            var typeInfo = self.GetTypeInfo();
            return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        
        public static bool Implements<T>(this Type self) => typeof(T).IsAssignableFrom(self);
        public static bool Extends<T>(this Type self) => typeof(T).IsAssignableFrom(self);
    }
}