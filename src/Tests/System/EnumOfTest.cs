using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.System
{
    public class EnumOfTest
    {
        enum Order
        {
            First,
            Second,
            Third,
            Fourth,
            Fifth
        }
        
        [Fact]
        public void Values_ShouldReturnAllEnumValues() =>
            EnumOf<Order>.Values
                .ShouldBe(new[] 
                { 
                    Order.First, 
                    Order.Second, 
                    Order.Third, 
                    Order.Fourth, 
                    Order.Fifth 
                });
    }
}