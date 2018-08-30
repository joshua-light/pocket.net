using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("Hello")]
        public void IsNullOrEmpty_ShouldBeFalse_IfStringIsNotNullOrEmpty(string value) =>
            value.IsNullOrEmpty().ShouldBeFalse();
        
        [Fact]
        public void IsNullOrEmpty_ShouldBeTrue_IfStringIsNull() =>
            ((string) null).IsNullOrEmpty().ShouldBeTrue();
        
        [Fact]
        public void IsNullOrEmpty_ShouldBeTrue_IfStringIsEmpty() =>
            "".IsNullOrEmpty().ShouldBeTrue();
    }
}