using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class TypeExtensionsTest
    {
        [Theory]
        [InlineData(typeof(int?))]
        [InlineData(typeof(long?))]
        [InlineData(typeof(short?))]
        [InlineData(typeof(byte?))]
        [InlineData(typeof(double?))]
        public void IsNullable_ShouldBeTrue_IfTypeIsNullable(Type type) => Assert.True(type.IsNullable());

        [Theory]
        [InlineData(typeof(int), typeof(int))]
        [InlineData(typeof(IEnumerable<>), typeof(IEnumerable<>))]
        [InlineData(typeof(IEnumerable<int>), typeof(IEnumerable<int>))]
        public void Is_ShouldBeTrue_IfTypesAreEqual(Type a, Type b) => Assert.True(a.Is(b));
        
        [Theory]
        [InlineData(typeof(int), typeof(double))]
        [InlineData(typeof(HashSet<>), typeof(List<>))]
        [InlineData(typeof(HashSet<int>), typeof(List<int>))]
        public void Is_ShouldBeFalse_IfTypesAreNotEqual(Type a, Type b) => Assert.False(a.Is(b));
        
        [Theory]
        [InlineData(typeof(List<int>), typeof(List<>))]
        [InlineData(typeof(Dictionary<int, string>), typeof(Dictionary<,>))]
        public void Is_ShouldBeTrue_IfSecondIsGenericDefinitionOfFirst(Type a, Type b) => Assert.True(a.Is(b));
        
        [Theory]
        [InlineData(typeof(int), typeof(List<>))]
        [InlineData(typeof(HashSet<int>), typeof(List<>))]
        [InlineData(typeof(IEnumerable<int>), typeof(List<>))]
        public void Is_ShouldBeFalse_IfSecondIsNotGenericDefinitionOfFirst(Type a, Type b) => Assert.False(a.Is(b));
        
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
        }
        
        [Fact]
        public void Extends_ShouldThrow_IfSpecifiedTypeIsNotClass() =>
            Assert.Throws<InvalidOperationException>(() => typeof(Woman).Extends(typeof(IAnimal)));
        
        #region Inner Classes
        
        private interface IAnimal { }
        private interface IOrganism { }

        private class Human : IAnimal, IOrganism { }
        private class Man : Human { }
        private class Woman : Human { }
        private class John : Man { }
        private class Jannet : Woman { }
        
        private class GenericParent<T> { }
        private class GenericChild<T> : GenericParent<T> { }
        private class GenericGrandChild<T> : GenericChild<T> { }

        #endregion
    }
}