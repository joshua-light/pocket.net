using System;
using System.Collections.Generic;
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
        ///     Represents structure that allows building declarative expressions of different <see cref="BindingFlags"/> values.
        /// </summary>
        public struct BindingSpecification
        {
            private readonly BindingFlags _flags;

            /// <summary>
            ///     Initializes new instance of <see cref="BindingSpecification"/> with specified flags.
            /// </summary>
            /// <param name="flags">Initial <see cref="BindingFlags"/> value.</param>
            public BindingSpecification(BindingFlags flags) =>
                _flags = flags;

            /// <summary>
            ///     Represents language construct that allows more fluent expressions.
            /// </summary>
            public BindingSpecification Or => this;
            
            /// <summary>
            ///     Represents language construct that allows more fluent expressions.
            /// </summary>
            public BindingSpecification And => this;

            /// <summary>
            ///     Returns instance of <see cref="BindingSpecification"/> that represents all (public or nonpublic) static fields.
            /// </summary>
            /// <returns>New instance of <see cref="BindingSpecification"/>.</returns>
            public BindingSpecification AllStatic() =>
                Public().Or.NonPublic().And.Static();
            
            /// <summary>
            ///     Returns instance of <see cref="BindingSpecification"/> that represents all (public or nonpublic) instance fields.
            /// </summary>
            /// <returns>New instance of <see cref="BindingSpecification"/>.</returns>
            public BindingSpecification AllInstance() =>
                Public().Or.NonPublic().And.Instance();
      
            /// <summary>
            ///     ORs current <see cref="BindingSpecification"/> value with <see cref="BindingFlags.Public"/>.
            /// </summary>
            /// <returns>New instance of <see cref="BindingSpecification"/>.</returns>
            public BindingSpecification Public() =>
                With(BindingFlags.Public);
            
            /// <summary>
            ///     ORs current <see cref="BindingSpecification"/> value with <see cref="BindingFlags.NonPublic"/>.
            /// </summary>
            /// <returns>New instance of <see cref="BindingSpecification"/>.</returns>
            public BindingSpecification NonPublic() =>
                With(BindingFlags.NonPublic);

            /// <summary>
            ///     ORs current <see cref="BindingSpecification"/> value with <see cref="BindingFlags.Static"/>.
            /// </summary>
            /// <returns>New instance of <see cref="BindingSpecification"/>.</returns>
            public BindingSpecification Static() =>
                With(BindingFlags.Static);
            
            /// <summary>
            ///     ORs current <see cref="BindingSpecification"/> value with <see cref="BindingFlags.Instance"/>.
            /// </summary>
            /// <returns>New instance of <see cref="BindingSpecification"/>.</returns>
            public BindingSpecification Instance() =>
                With(BindingFlags.Instance);
      
            /// <summary>
            ///     Implicitly casts <see cref="BindingSpecification"/> to <see cref="BindingFlags"/>.
            /// </summary>
            /// <param name="self"><code>this</code> object.</param>
            /// <returns>Instance of <see cref="BindingFlags"/> constructed from <paramref name="self"/>.</returns>
            public static implicit operator BindingFlags(BindingSpecification self) =>
                self._flags;
            
            private BindingSpecification With(BindingFlags flags) =>
                new BindingSpecification(_flags | flags);
        }
        
        public struct BoundedFieldInfo
        {
            public readonly object _this;
            public readonly FieldInfo _field;

            public BoundedFieldInfo(object @this, FieldInfo field)
            {
                _this = @this;
                _field = field;
            }

            public FieldInfo Info =>
                _field;

            public T As<T>() => (T)
                _field.GetValue(_this);

            public void Set(object value) =>
                _field.SetValue(_this, value);
        }
        
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
            
            return interfaces.Any(x => (x.IsConstructedGenericType ? x.GetGenericTypeDefinition() : x) == other);
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

            var  isConstructed = self.BaseType?.IsConstructedGenericType ?? false;
            if ((isConstructed ? self.BaseType.GetGenericTypeDefinition() : self.BaseType) == other)
                return true;
            
            if (!self.IsGenericType)
                return self.BaseType?.Extends(other) ?? false;

            return self.IsGenericTypeDefinition
                ? self.BaseType.Extends(other)
                : self.GetGenericTypeDefinition().BaseType.Extends(other);
        }
    
        /// <summary>
        ///     Gets all (static and instance) public fields of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Public static and public instance fields of <paramref name="self"/> type.</returns>
        public static FieldInfo[] Fields(this Type self) => self.GetFields();

        /// <summary>
        ///     Gets all (static and instance) public fields of specified type bounded to specified object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="obj">Object of type <paramref name="self"/> that holds fields values.</param>
        /// <returns>Public static and public instance fields of <paramref name="self"/> type.</returns>
        public static BoundedFieldInfo[] Fields(this Type self, object obj) =>
            self.Fields().Select(x => new BoundedFieldInfo(obj, x)).ToArray();
        
        /// <summary>
        ///     Gets fields configured by <see cref="BindingSpecification"/> of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="specify">Function that configures <see cref="BindingSpecification"/> object.</param>
        /// <returns>Fields of <paramref name="self"/> type.</returns>
        public static FieldInfo[] Fields(this Type self, Func<BindingSpecification, BindingSpecification> specify) =>
            self.GetFields(specify(new BindingSpecification()));
    
        /// <summary>
        ///     Gets all (static and instance) public methods of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Public static and public instance methods of <paramref name="self"/> type.</returns>
        public static MethodInfo[] Methods(this Type self) => self.GetMethods();
        
        /// <summary>
        ///     Gets mehtods configured by <see cref="BindingSpecification"/> of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="specify">Function that configures <see cref="BindingSpecification"/> object.</param>
        /// <returns>Methods of <paramref name="self"/> type.</returns>
        public static MethodInfo[] Methods(this Type self, Func<BindingSpecification, BindingSpecification> specify) =>
            self.GetMethods(specify(new BindingSpecification()));

        /// <summary>
        ///     Gets all (public and nonpublic) instance fields of specified type that are marked with <typeparamref name="T"/> attribute.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of attribute that field should be marked with.</typeparam>
        /// <returns>Fields of <paramref name="self"/> type that are marked with <typeparamref name="T"/> attribute.</returns>
        public static IEnumerable<FieldInfo> FieldsWith<T>(this Type self) where T : Attribute =>
            self
                .Fields(_ => _.AllInstance())
                .Where(x => x.GetCustomAttribute<T>() != null);
        
        public static string PrettyName(this Type self)
        {
            if (self.IsGenericType)
            {
                var arguments = self.GetGenericArguments();
                var argumentsText = new StringParts();

                for (var i = 0; i < arguments.Length; i++)
                {
                    var argument = arguments[i];

                    argumentsText.With(self.IsConstructedGenericType ? argument.PrettyName() : "");
                
                    if (i != arguments.Length - 1)
                        argumentsText.With(self.IsConstructedGenericType ? ", " : ",");
                }

                return $"{self.Name.Replace($"`{arguments.Length}", "")}<{argumentsText}>";
            }

            if (self.IsArray)
            {
                var rank = self.GetArrayRank();
                var commas = new StringParts();

                for (var i = 0; i < rank - 1; i++)
                    commas.With(",");

                return $"{self.GetElementType().PrettyName()}[{commas}]";
            }
            
            switch (self.FullName)
            {
                case "System.Boolean":  return "bool";
                case "System.Byte":     return "byte";
                case "System.SByte":    return "sbyte";
                case "System.Int16":    return "short";
                case "System.UInt16":   return "ushort";
                case "System.Int32":    return "int";
                case "System.UInt32":   return "uint";
                case "System.Int64":    return "long";
                case "System.UInt64":   return "ulong";
                case "System.Single":   return "float";
                case "System.Double":   return "double";
                case "System.Decimal":  return "decimal";
                case "System.String":   return "string";
                case "System.Object":   return "object";
                
                default: return self.Name;
            }
        }
    }
}