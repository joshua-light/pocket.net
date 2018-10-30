using System;
using System.Linq;
using System.Reflection;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Checks whether specified type is <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is <see cref="Nullable{T}"/>, otherwise <code>false</code>.</returns>
        public static bool IsNullable(this Type self)
        {
            var typeInfo = self.GetTypeInfo();
            return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        ///     Checks whether <paramref name="self"/> is somehow equal to <paramref name="other"/>.
        /// </summary>
        /// <remarks>
        ///     Types can be equal by direct equality e.g. <code>typeof(int).Is(typeof(int))</code>
        ///     or by generics definitions: <code>typeof(List{int}).Is(typeof(List{}))</code>.
        /// </remarks>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Type that will be checked for equality to <code>this</code>.</param>
        /// <returns><code>true</code> if types are equal, otherwise <code>false</code>.</returns>
        public static bool Is(this Type self, Type other)
        {
            if (other.IsGenericTypeDefinition)
                return self.IsGenericType && self.GetGenericTypeDefinition() == other;

            return self == other;
        }

        /// <summary>
        ///     Checks whether <paramref name="self"/> implements <paramref name="other"/> at type level.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Type of interface that will be checked for implementation by <paramref name="self"/>.</param>
        /// <returns><code>true</code> if <paramref name="self"/> implements <paramref name="other"/>, otherwise <code>false</code>.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="other"/> is not an interface type.</exception>
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

        /// <summary>
        ///     Checks whether <paramref name="self"/> extends <paramref name="other"/> at type level.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other">Type that will be checked for inheritance.</param>
        /// <returns><code>true</code> if <paramref name="self"/> extends <paramref name="other"/>, otherwise <code>false</code>.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="other"/> is not a class.</exception>
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