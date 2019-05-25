using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Pocket.Common.Guard;

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

            public BindingSpecification OfThatType() =>
                With(~BindingFlags.FlattenHierarchy);
      
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
            private readonly object _this;
            private readonly FieldInfo _field;

            public BoundedFieldInfo(object @this, FieldInfo field)
            {
                _this = @this;
                _field = field;
            }

            public FieldInfo Info =>
                _field;

            public object Value =>
                _field.GetValue(_this);

            public T As<T>() => (T)
                _field.GetValue(_this);

            public void Set(object value) =>
                _field.SetValue(_this, value);
        }
        
        public struct BoundedPropertyInfo
        {
            public readonly object _this;
            public readonly PropertyInfo _property;

            public BoundedPropertyInfo(object @this, PropertyInfo property)
            {
                _this = @this;
                _property = property;
            }

            public PropertyInfo Info =>
                _property;

            public object Value =>
                _property.GetValue(_this);
            
            public T As<T>() => (T)
                _property.GetValue(_this);

            public void Set(object value) =>
                _property.SetValue(_this, value);
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

        public static bool Is<T>(this Type self) => self.Is(typeof(T));

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

        public static bool Derives<T>(this Type self) => self.Derives(typeof(T));
        public static bool Derives(this Type self, Type from) =>
            from.IsInterface ? self.Implements(from) : self.Extends(from);

        /// <summary>
        ///     Checks whether <paramref name="self"/> implements <typeparamref name="T"/> at type level.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of interface that will be checked for implementation by <paramref name="self"/>.</typeparam>
        /// <returns><code>true</code> if <paramref name="self"/> implements <typeparamref name="T"/>, otherwise <code>false</code>.</returns>
        /// <exception cref="InvalidOperationException"><typeparamref name="T"/> is not an interface type.</exception>
        public static bool Implements<T>(this Type self) =>
            self.Implements(typeof(T));

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
            
            if (self == other)
                return false;
            
            if (!other.IsGenericTypeDefinition)
                return other.IsAssignableFrom(self);

            var interfaces = !self.IsGenericType || self.IsGenericTypeDefinition
                ? self.GetTypeInfo().ImplementedInterfaces
                : self.GetGenericTypeDefinition().GetTypeInfo().ImplementedInterfaces;
            
            return interfaces.Any(x => (x.IsConstructedGenericType ? x.GetGenericTypeDefinition() : x) == other);
        }

        /// <summary>
        ///     Checks whether <paramref name="self"/> extends <typeparamref name="T"/> at type level.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type that will be checked for inheritance.</typeparam>
        /// <returns><code>true</code> if <paramref name="self"/> extends <typeparamref name="T"/>, otherwise <code>false</code>.</returns>
        /// <exception cref="InvalidOperationException"><typeparamref name="T"/> is not a class.</exception>
        public static bool Extends<T>(this Type self) =>
            self.Extends(typeof(T));

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
                throw new InvalidOperationException($"Specified {other.Name} is not a class.");
            
            if (self == other)
                return false;
            
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
        ///     Gets fields configured by <see cref="BindingSpecification"/> of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="specify">Function that configures <see cref="BindingSpecification"/> object.</param>
        /// <returns>Fields of <paramref name="self"/> type.</returns>
        public static FieldInfo[] Fields(this Type self, Func<BindingSpecification, BindingSpecification> specify) =>
            self.GetFields(specify(new BindingSpecification()));
        
        /// <summary>
        ///     Gets all (static and instance) public fields of specified type bounded to specified object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="of">Object of type <paramref name="self"/> that holds fields values.</param>
        /// <returns>Public static and public instance fields of <paramref name="self"/> type.</returns>
        public static BoundedFieldInfo[] Fields(this Type self, object of) =>
            self.Fields().Select(x => new BoundedFieldInfo(of, x)).ToArray();
        
        /// <summary>
        ///     Gets fields configured by <see cref="BindingSpecification"/> of specified type bounded to specified object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="of">Object of type <paramref name="self"/> that holds fields values.</param>
        /// <returns>Public static and public instance fields of <paramref name="self"/> type.</returns>
        public static BoundedFieldInfo[] Fields(this Type self, object of, Func<BindingSpecification, BindingSpecification> that) =>
            self.Fields(that).Select(x => new BoundedFieldInfo(of, x)).ToArray();
    
        /// <summary>
        ///     Gets all (public and nonpublic) instance fields of specified type that are marked with <typeparamref name="T"/> attribute.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of attribute that field should be marked with.</typeparam>
        /// <returns>Fields of <paramref name="self"/> type that are marked with <typeparamref name="T"/> attribute.</returns>
        public static IEnumerable<FieldInfo> FieldsWith<T>(this Type self) where T : Attribute =>
            self
                .Fields(_ => _.AllInstance())
                .Where(x => x.Has<T>());
        
        /// <summary>
        ///     Gets all (static and instance) public properties of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Public static and public instance properties of <paramref name="self"/> type.</returns>
        public static PropertyInfo[] Properties(this Type self) => self.GetProperties();
        
        /// <summary>
        ///     Gets properties configured by <see cref="BindingSpecification"/> of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="that">Function that configures <see cref="BindingSpecification"/> object.</param>
        /// <returns>Properties of <paramref name="self"/> type.</returns>
        public static PropertyInfo[] Properties(this Type self, Func<BindingSpecification, BindingSpecification> that) =>
            self.GetProperties(that(new BindingSpecification()));
        
        /// <summary>
        ///     Gets all (static and instance) public properties of specified type bounded to specified object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="obj">Object of type <paramref name="self"/> that holds fields values.</param>
        /// <returns>Public static and public instance properties of <paramref name="self"/> type.</returns>
        public static BoundedPropertyInfo[] Properties(this Type self, object obj) =>
            self.Properties().Select(x => new BoundedPropertyInfo(obj, x)).ToArray();
        
        /// <summary>
        ///     Gets properties configured by <see cref="BindingSpecification"/> of specified type bounded to specified object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="obj">Object of type <paramref name="self"/> that holds fields values.</param>
        /// <returns>Public static and public instance properties of <paramref name="self"/> type.</returns>
        public static BoundedPropertyInfo[] Properties(this Type self, object of, Func<BindingSpecification, BindingSpecification> that) =>
            self.Properties(that).Select(x => new BoundedPropertyInfo(of, x)).ToArray();
        
        /// <summary>
        ///     Gets all (public and nonpublic) instance properties of specified type that are marked with <typeparamref name="T"/> attribute.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of attribute that property should be marked with.</typeparam>
        /// <returns>Properties of <paramref name="self"/> type that are marked with <typeparamref name="T"/> attribute.</returns>
        public static IEnumerable<PropertyInfo> PropertiesWith<T>(this Type self) where T : Attribute =>
            self
                .Properties(_ => _.AllInstance())
                .Where(x => x.Has<T>());
        
        /// <summary>
        ///     Gets all (static and instance) public methods of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Public static and public instance methods of <paramref name="self"/> type.</returns>
        public static MethodInfo[] Methods(this Type self) => self.GetMethods();
        
        /// <summary>
        ///     Gets methods configured by <see cref="BindingSpecification"/> of specified type.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="specify">Function that configures <see cref="BindingSpecification"/> object.</param>
        /// <returns>Methods of <paramref name="self"/> type.</returns>
        public static MethodInfo[] Methods(this Type self, Func<BindingSpecification, BindingSpecification> specify) =>
            self.GetMethods(specify(new BindingSpecification()));
        
        /// <summary>
        ///     Gets all (public and nonpublic) instance methods of specified type that are marked with <typeparamref name="T"/> attribute.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <typeparam name="T">Type of attribute that method should be marked with.</typeparam>
        /// <returns>Methods of <paramref name="self"/> type that are marked with <typeparamref name="T"/> attribute.</returns>
        public static IEnumerable<MethodInfo> MethodsWith<T>(this Type self) where T : Attribute =>
            self
                .Methods(_ => _.AllInstance())
                .Where(x => x.Has<T>());

        public static string PrettyName(this Type self, bool asNested = false) =>
            self.PrettyName(asNested, context: null);
        public static string PrettyName(this Type self, Type context) =>
            self.PrettyName(asNested: true, context);
        
        /// <summary>
        ///     Name of the type (same as <code>Name</code> property) but with correct generic arguments.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns>Name of the type.</returns>
        private static string PrettyName(this Type self, bool asNested, Type context)
        {
            var name = default(string);
            
            if (self.IsNullable())
                name = $"{self.GetGenericArguments().First().PrettyName(asNested, context)}?";
            else
            if (self.IsGenericType)
            {
                var arguments = self.GetGenericArguments();
                var argumentsText = arguments
                    .Select(x => x.PrettyName(asNested: true, context))
                    .Separated(with: ", ");

                name = $"{self.Name.Replace($"`{arguments.Length}", "")}<{argumentsText}>";
            }
            else
            if (self.IsArray)
            {
                var rank = self.GetArrayRank();
                var commas = new string(',', rank - 1);

                name = $"{self.GetElementType().PrettyName(asNested, context)}[{commas}]";
            }
            else
            switch (self.FullName)
            {
                case "System.Boolean":  name = "bool"; break;
                case "System.Byte":     name = "byte"; break;
                case "System.SByte":    name = "sbyte"; break;
                case "System.Int16":    name = "short"; break;
                case "System.UInt16":   name = "ushort"; break;
                case "System.Int32":    name = "int"; break;
                case "System.UInt32":   name = "uint"; break;
                case "System.Int64":    name = "long"; break;
                case "System.UInt64":   name = "ulong"; break;
                case "System.Single":   name = "float"; break;
                case "System.Double":   name = "double"; break;
                case "System.Decimal":  name = "decimal"; break;
                case "System.String":   name = "string"; break;
                case "System.Object":   name = "object"; break;
                
                default: name = self.Name; break;
            }
            
            if (asNested && self.IsNested && !self.IsGenericParameter)
                name = $"{self.NestedName(context)}";
            
            return name;
        }

        private static string NestedName(this Type self, Type context = null)
        {
            return WithDeclaringTypes(self)
                .Except(WithDeclaringTypes(context))
                .Select(x => x.PrettyName())
                .Reverse()
                .Separated(".");
      
            IEnumerable<Type> WithDeclaringTypes(Type x)
            {
                if (x == null)
                    yield break;
                
                yield return x;
        
                while (x.DeclaringType != null)
                    yield return x = x.DeclaringType;
            }
        }
        
        public static object New(this Type self) =>
            Activator.CreateInstance(self);

        public static T New<T>(this Type self) => (T) self.New(@as: typeof(T));
        public static object New(this Type self, Type @as)
        {
            if (!self.IsGenericTypeDefinition)
                return self.New();
            if (!@as.IsConstructedGenericType)
                throw new ArgumentException($"Couldn't create [ {self.PrettyName()} ] as [ {@as.PrettyName()} ]: " +
                                            $"second one is not constructed generic type.");

            var asDefinition = @as.GetGenericTypeDefinition();
            if (asDefinition != self)
            {
                if (@as.IsInterface && !self.Implements(asDefinition))
                    throw new ArgumentException($"Couldn't create [ {self.PrettyName()} ] as [ {@as.PrettyName()} ]: " +
                                                $"first doesn't implement second.");
                if (@as.IsClass && self != asDefinition && !self.Extends(asDefinition))
                    throw new ArgumentException($"Couldn't create [ {self.PrettyName()} ] as [ {@as.PrettyName()} ]: " +
                                                $"first doesn't extend second.");    
            }
            
            return self.MakeGenericType(@as.GetGenericArguments()).New();
        }
        
        public struct EnumType
        {
            private readonly List<(string Name, object Value)> _values;

            public EnumType(Type type)
            {
                Ensure(type.IsEnum);
                
                var underlying = type.GetEnumUnderlyingType();
                
                Underlying = underlying;

                _values = type
                    .GetEnumValues()
                    .Cast<object>()
                    .Select(x => (x.ToString(), Convert.ChangeType(x, underlying)))
                    .ToList();
            }

            public Type Underlying { get; }

            public IReadOnlyList<(string Name, object Value)> Values => _values;
        }
        
        public static EnumType Enum(this Type self) =>
            new EnumType(self);
    }
}