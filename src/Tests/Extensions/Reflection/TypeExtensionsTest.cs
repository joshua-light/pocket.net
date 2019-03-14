using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions.Reflection
{
    public class TypeExtensionsTest
    {
        #region IsNullable
        
        [Theory]
        [InlineData(typeof(int?))]
        [InlineData(typeof(long?))]
        [InlineData(typeof(short?))]
        [InlineData(typeof(byte?))]
        [InlineData(typeof(double?))]
        public void IsNullable_ShouldBeTrue_IfTypeIsNullable(Type type) => Assert.True(type.IsNullable());
        
        #endregion

        #region Is

        [Theory]
        [InlineData(typeof(int), typeof(int))]
        [InlineData(typeof(IEnumerable<>), typeof(IEnumerable<>))]
        [InlineData(typeof(IEnumerable<int>), typeof(IEnumerable<int>))]
        public void Is_ShouldBeTrue_IfTypesAreEqual(Type a, Type b) =>
            Assert.True(a.Is(b));
        
        [Theory]
        [InlineData(typeof(int), typeof(double))]
        [InlineData(typeof(HashSet<>), typeof(List<>))]
        [InlineData(typeof(HashSet<int>), typeof(List<int>))]
        public void Is_ShouldBeFalse_IfTypesAreNotEqual(Type a, Type b) =>
            Assert.False(a.Is(b));
        
        [Theory]
        [InlineData(typeof(List<int>), typeof(List<>))]
        [InlineData(typeof(Dictionary<int, string>), typeof(Dictionary<,>))]
        public void Is_ShouldBeTrue_IfSecondIsGenericDefinitionOfFirst(Type a, Type b) =>
            Assert.True(a.Is(b));
        
        [Theory]
        [InlineData(typeof(int), typeof(List<>))]
        [InlineData(typeof(HashSet<int>), typeof(List<>))]
        [InlineData(typeof(IEnumerable<int>), typeof(List<>))]
        public void Is_ShouldBeFalse_IfSecondIsNotGenericDefinitionOfFirst(Type a, Type b) =>
            Assert.False(a.Is(b));

        #endregion

        #region Implements

        [Fact]
        public void Implements_ShouldBeFalse_IfTypeIsSelf() =>
            typeof(IAnimal).Implements<IAnimal>().ShouldBeFalse();

        [Fact]
        public void Implements_ShouldBeTrue_IfTypeImplementsInterface()
        {
            Assert.True(typeof(Human).Implements(typeof(IAnimal)));
            Assert.True(typeof(Human).Implements(typeof(IOrganism)));
            
            Assert.True(typeof(Woman).Implements(typeof(IAnimal)));
            Assert.True(typeof(Woman).Implements(typeof(IOrganism)));
        }
        
        [Fact]
        public void Implements_ShouldBeFalse_IfTypeDoesntImplementsInterface()
        {
            Assert.False(typeof(Human).Implements(typeof(IEnumerable<>)));
            Assert.False(typeof(Woman).Implements(typeof(ICollection)));
            Assert.False(typeof(Woman).Implements(typeof(ICollection<>)));
            Assert.False(typeof(Woman).Implements(typeof(IEnumerable<int>)));
        }
        
        [Fact]
        public void Implements_ShouldBeTrue_IfGenericTypeImplementsInterface()
        {
            Assert.True(typeof(HashSet<int>).Implements(typeof(IEnumerable<>)));
            Assert.True(typeof(HashSet<>).Implements(typeof(IEnumerable<>)));
            Assert.True(typeof(List<>).Implements(typeof(IEnumerable<>)));
            Assert.True(typeof(Dictionary<,>).Implements(typeof(IEnumerable<>)));
        }

        [Fact]
        public void Implements_ShouldThrow_IfSpecifiedTypeIsNotInterface() =>
            Assert.Throws<InvalidOperationException>(() => typeof(Woman).Implements(typeof(Human)));

        #endregion

        #region Extends

        [Fact]
        public void Extends_ShouldBeTrue_IfTypeExtendsClass()
        {
            Assert.True(typeof(Man).Extends(typeof(Human)));
            Assert.True(typeof(Woman).Extends(typeof(Human)));
            Assert.True(typeof(John).Extends(typeof(Human)));
            Assert.True(typeof(Jannet).Extends(typeof(Human)));
        }
        
        [Fact]
        public void Extends_ShouldBeFalse_IfTypeDoesntExtendsClass()
        {
            Assert.False(typeof(Man).Extends(typeof(Woman)));
            Assert.False(typeof(Woman).Extends(typeof(Man)));
            Assert.False(typeof(John).Extends(typeof(List<>)));
            Assert.False(typeof(Jannet).Extends(typeof(List<int>)));
            Assert.False(typeof(Jannet).Extends(typeof(HashSet<>)));
        }
        
        [Fact]
        public void Extends_ShouldBeTrue_IfGenericTypeExtendsClass()
        {
            Assert.True(typeof(GenericChild<int>).Extends(typeof(GenericParent<>)));
            Assert.True(typeof(GenericChild<>).Extends(typeof(GenericParent<>)));
            
            Assert.True(typeof(GenericGrandChild<int>).Extends(typeof(GenericParent<>)));
            Assert.True(typeof(GenericGrandChild<>).Extends(typeof(GenericParent<>)));
            Assert.True(typeof(ConcreteChild).Extends(typeof(GenericChild<>)));
        }
        
        [Fact]
        public void Extends_ShouldThrow_IfSpecifiedTypeIsNotClass() =>
            Assert.Throws<InvalidOperationException>(() => typeof(Woman).Extends(typeof(IAnimal)));

        #endregion
        
        #region PrettyName

        [Fact]
        public void PrettyName_ShouldReturnTypeName_IfTypeIsNonGeneric() =>
            typeof(Man).PrettyName().ShouldBe("Man");
        
        [Fact]
        public void PrettyNameWithDeclaringType_ShouldReturnFullTypeName_IfTypeIsNested() =>
            typeof(Item.Nested).PrettyName(asNested: true).ShouldBe("Item.Nested");
        
        [Fact]
        public void PrettyNameWithDeclaringType_ShouldReturnFullTypeName_IfTypeIsDoubleNested() =>
            typeof(Item.Nested.Again).PrettyName(asNested: true).ShouldBe("Item.Nested.Again");

        [Theory]
        [InlineData(typeof(int?), "int?")]
        [InlineData(typeof(BindingFlags?), "BindingFlags?")]
        [InlineData(typeof(DateTime?), "DateTime?")]
        [InlineData(typeof(GenericStruct<int>?), "GenericStruct<int>?")]
        [InlineData(typeof(GenericStruct<int?>?), "GenericStruct<int?>?")]
        public void PrettyName_ShouldReturnShortTypeName_IfTypeIsNullable(Type type, string name) =>
            type.PrettyName().ShouldBe(name);
        
        [Theory]
        [InlineData(typeof(IEnumerable<bool>),    "IEnumerable<bool>")]
        [InlineData(typeof(IEnumerable<byte>),    "IEnumerable<byte>")]
        [InlineData(typeof(IEnumerable<sbyte>),   "IEnumerable<sbyte>")]
        [InlineData(typeof(IEnumerable<short>),   "IEnumerable<short>")]
        [InlineData(typeof(IEnumerable<ushort>),  "IEnumerable<ushort>")]
        [InlineData(typeof(IEnumerable<int>),     "IEnumerable<int>")]
        [InlineData(typeof(IEnumerable<uint>),    "IEnumerable<uint>")]
        [InlineData(typeof(IEnumerable<long>),    "IEnumerable<long>")]
        [InlineData(typeof(IEnumerable<ulong>),   "IEnumerable<ulong>")]
        [InlineData(typeof(IEnumerable<float>),   "IEnumerable<float>")]
        [InlineData(typeof(IEnumerable<double>),  "IEnumerable<double>")]
        [InlineData(typeof(IEnumerable<decimal>), "IEnumerable<decimal>")]
        [InlineData(typeof(IEnumerable<string>),  "IEnumerable<string>")]
        [InlineData(typeof(IEnumerable<object>),  "IEnumerable<object>")]
        public void PrettyName_ShouldReturnTypeNameWithGenericParameters_IfTypeIsConstructedGenericAndOneParameterIsUsed(Type type, string expected) =>
            type.PrettyName().ShouldBe(expected);
        
        [Fact]
        public void PrettyName_ShouldReturnTypeNameWithGenericParameters_IfTypeIsUnconstructedGenericAndOneParameterIsUsed() =>
            typeof(IEnumerable<>).PrettyName().ShouldBe("IEnumerable<T>");
        
        [Fact]
        public void PrettyName_ShouldReturnTypeNameWithGenericParameters_IfTypeIsConstructedGenericAndTwoParametersAreUsed() =>
            typeof(IDictionary<int, string>).PrettyName().ShouldBe("IDictionary<int, string>");
        
        [Fact]
        public void PrettyName_ShouldReturnTypeNameWithGenericParameters_IfTypeIsUnconstructedGenericAndTwoParametersAreUsed() =>
            typeof(IDictionary<,>).PrettyName().ShouldBe("IDictionary<TKey, TValue>");
        
        [Fact]
        public void PrettyName_ShouldReturnCorrectTypeName_IfTypeIsArray() =>
            typeof(int[]).PrettyName().ShouldBe("int[]");
        
        [Fact]
        public void PrettyName_ShouldReturnCorrectTypeName_IfTypeIsArrayOfRankTwo() =>
            typeof(int[,]).PrettyName().ShouldBe("int[,]");
        
        [Fact]
        public void PrettyName_ShouldReturnCorrectTypeName_IfTypeIsArrayOfRankThree() =>
            typeof(int[,,]).PrettyName().ShouldBe("int[,,]");
        
        [Fact]
        public void PrettyName_ShouldReturnCorrectTypeName_IfTypeIsArrayOfArrays() =>
            typeof(int[][]).PrettyName().ShouldBe("int[][]");
        
        [Fact]
        public void PrettyName_ShouldReturnCorrectTypeName_IfTypeIsArrayOfArraysOfRankTwo() =>
            typeof(int[,][]).PrettyName().ShouldBe("int[][,]");
        
        [Fact]
        public void PrettyName_ShouldReturnCorrectTypeName_IfTypeIsArrayOfRankTwoOfArrays() =>
            typeof(int[][,]).PrettyName().ShouldBe("int[,][]");
        
        [Fact]
        public void PrettyName_ShouldReturnCorrectTypeName_IfTypeIsArrayOfRankTwoOfArraysOfRankTwo() =>
            typeof(int[,][,]).PrettyName().ShouldBe("int[,][,]");

        #endregion
    }
    
    public interface IAnimal { }
    public interface IOrganism { }

    public class Human : IAnimal, IOrganism { }
    public class Man : Human { }
    public class Woman : Human { }
    public class John : Man { }
    public class Jannet : Woman { }

    public class Item { public class Nested { public class Again { } } }
        
    public class GenericParent<T> { }
    public class GenericChild<T> : GenericParent<T> { }
    public class GenericGrandChild<T> : GenericChild<T> { }
    public class ConcreteChild : GenericChild<int> { }
    public struct GenericStruct<T> { }
}