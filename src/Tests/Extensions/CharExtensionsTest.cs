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
            a.Is(b).ShouldBeTrue();
        
        [Theory]
        [InlineData('1', '2')]
        [InlineData('2', '3')]
        [InlineData('3', '4')]
        [InlineData('4', '5')]
        public void Is_ShouldReturnFalse_IfCharactersAreNotEqual(char a, char b) =>
            a.Is(b).ShouldBeFalse();
        
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
        [InlineData('\n')]
        [InlineData('\r')]
        [InlineData(')')]
        public void IsDigit_ShouldBeFalse_IfCharIsNotDigit(char ch) =>
            ch.IsDigit().ShouldBeFalse();

        [Theory]
        [InlineData('a')]
        [InlineData('B')]
        [InlineData('w')]
        [InlineData('e')]
        [InlineData('D')]
        public void IsLetter_ShouldBeTrue_IfCharIsLetter(char ch) =>
            ch.IsLetter().ShouldBeTrue();
        
        [Theory]
        [InlineData('1')]
        [InlineData(',')]
        [InlineData('-')]
        [InlineData('#')]
        [InlineData('\n')]
        public void IsLetter_ShouldBeFalse_IfCharIsNotLetter(char ch) =>
            ch.IsLetter().ShouldBeFalse();
        
        [Theory]
        [InlineData('a')]
        [InlineData('1')]
        [InlineData('W')]
        [InlineData('4')]
        [InlineData('D')]
        public void IsLetter_ShouldBeTrue_IfCharIsLetterOrDigit(char ch) =>
            ch.IsLetterOrDigit().ShouldBeTrue();
        
        [Theory]
        [InlineData(',')]
        [InlineData('-')]
        [InlineData('#')]
        [InlineData('\n')]
        public void IsLetter_ShouldBeFalse_IfCharIsNotLetterOrDigit(char ch) =>
            ch.IsLetterOrDigit().ShouldBeFalse();
    }
}