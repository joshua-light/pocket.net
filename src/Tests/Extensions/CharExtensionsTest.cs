using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class CharExtensionsTest
    {
        [Theory]
        [InlineData('0')]
        [InlineData('1')]
        [InlineData('2')]
        [InlineData('3')]
        [InlineData('4')]
        [InlineData('5')]
        [InlineData('6')]
        [InlineData('7')]
        [InlineData('8')]
        [InlineData('9')]
        public void IsDigit_ShouldBeTrue_IfCharIsDigit(char ch) =>
            ch.IsDigit().ShouldBeTrue();
        
        [Theory]
        [InlineData('`')]
        [InlineData('a')]
        [InlineData('b')]
        [InlineData('c')]
        [InlineData('\n')]
        [InlineData('\r')]
        [InlineData(')')]
        public void IsDigit_ShouldBeFalse_IfCharIsNotDigit(char ch) =>
            ch.IsDigit().ShouldBeFalse();
    }
}