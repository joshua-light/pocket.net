using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class CharExtensionsTest
    {
        [Theory]
        [InlineData('1', '1')]
        [InlineData('2', '2')]
        [InlineData('3', '3')]
        [InlineData('4', '4')]
        public void Is_ShouldReturnTrue_IfCharactersAreEqual(char a, char b) =>
            a.Is(b).ShouldBe(true);
        
        [Theory]
        [InlineData('1', '2')]
        [InlineData('2', '3')]
        [InlineData('3', '4')]
        [InlineData('4', '5')]
        public void Is_ShouldReturnFalse_IfCharactersAreNotEqual(char a, char b) =>
            a.Is(b).ShouldBe(false);
        
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