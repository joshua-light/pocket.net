using System;
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
        
        [Fact]
        public void Implements_ShouldBeTrue_IfTypeImplementsInterface()
        {
            Assert.True(typeof(Human).Implements(typeof(IAnimal)));
            Assert.True(typeof(Human).Implements(typeof(IOrganism)));
            
            Assert.True(typeof(Woman).Implements(typeof(IAnimal)));
            Assert.True(typeof(Woman).Implements(typeof(IOrganism)));
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
        public void Extends_ShouldBeTrue_IfGenericTypeExtendsClass()
        {
            Assert.True(typeof(GenericChild<int>).Extends(typeof(GenericParent<>)));
            Assert.True(typeof(GenericChild<>).Extends(typeof(GenericParent<>)));
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

        #endregion
    }
}