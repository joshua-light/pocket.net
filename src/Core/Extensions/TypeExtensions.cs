using System;
using System.Linq;
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

        public static bool Implements(this Type self, Type other)
        {
            if (!other.IsInterface)
                throw new InvalidOperationException($"Specified {other.Name} is not an interface.");
            
            if (!other.IsGenericTypeDefinition)
                return other.IsAssignableFrom(self);

            var interfaces = !self.IsGenericType || self.IsGenericTypeDefinition
                ? self.GetTypeInfo().ImplementedInterfaces
                : self.GetGenericTypeDefinition().GetTypeInfo().ImplementedInterfaces;
            
            // There is a strange thing:
            // type references in `interfaces` collection can look same as `other`, but actually are not.
            return interfaces.Any(x => x.GUID == other.GUID);
        }

        public static bool Extends(this Type self, Type other)
        {
            if (!other.IsClass)
                throw new InvalidOperationException($"Specified {other.Name} is not an interface.");
            
            if (!other.IsGenericTypeDefinition)
                return other.IsAssignableFrom(self);

            if (!self.IsGenericType)
                return false;

            if (self.BaseType.GUID == other.GUID)
                return true;

            return self.IsGenericTypeDefinition
                ? self.BaseType.Extends(other)
                : self.GetGenericTypeDefinition().BaseType.Extends(other);
        }
    }
}