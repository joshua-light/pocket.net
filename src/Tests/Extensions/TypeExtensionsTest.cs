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