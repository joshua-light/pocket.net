using System;
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
        public void Implements_ShouldBeTrue_IfTypeImplementsInterfaceOrExtendsClass()
        {
            Assert.True(typeof(Human).Implements<IAnimal>());
            Assert.True(typeof(Human).Implements<IOrganism>());
            
            Assert.True(typeof(Woman).Implements<IAnimal>());
            Assert.True(typeof(Woman).Implements<IOrganism>());
            Assert.True(typeof(Woman).Implements<Human>());
        }
        
        [Fact]
        public void Extends_ShouldBeTrue_IfTypeExtendsClassOrImplementsInterface()
        {
            Assert.True(typeof(Human).Extends<IAnimal>());
            Assert.True(typeof(Human).Extends<IOrganism>());
            
            Assert.True(typeof(Man).Extends<IAnimal>());
            Assert.True(typeof(Man).Extends<IOrganism>());
            Assert.True(typeof(Man).Extends<Human>());
        }
        
        #region Inner Classes
        
        private interface IAnimal { }
        private interface IOrganism { }

        private class Human : IAnimal, IOrganism { }
        private class Man : Human { }
        private class Woman : Human { }

        #endregion
    }
}