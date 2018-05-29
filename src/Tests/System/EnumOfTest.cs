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
        public void Values_ShouldReturnAllEnumValues()
        {
            var values = EnumOf<Order>.Values;
            Assert.Equal(new[] { Order.First, Order.Second, Order.Third, Order.Fourth, Order.Fifth }, values);
        }
    }
}