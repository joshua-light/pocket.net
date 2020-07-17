using Pocket.System;
using Shouldly;
using Xunit;

namespace Pocket.Tests.System
{
    public class EnumOfTest
    {
        private enum Order
        {
            First,
            Second,
            Third,
        }
        
        [Fact]
        public void Values_ReturnsAllEnumValuesAsArray() =>
            EnumOf<Order>.Values
                .ShouldBe(new[] 
                { 
                    Order.First, 
                    Order.Second, 
                    Order.Third, 
                });
        
        [Fact]
        public void From_ReturnsEnumValue_IfStringDoesMatch() =>
            EnumOf<Order>.From("First").ShouldBe(Order.First);
        
        [Fact]
        public void From_ReturnsNull_IfStringDoesNotMatch() =>
            EnumOf<Order>.From("").ShouldBe(null);
    }
}