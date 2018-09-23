using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class StringExtensionsTest
    {
        #region IsNullOrEmpty
        
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
        
        #endregion

        #region Or

        [Fact]
        public void Or_ShouldBeSelf_IfStringIsNotNull() =>
            "Hello".Or("1").ShouldBe("Hello");

        [Fact]
        public void Or_ShouldBeDefault_IfStringIsNull() =>
            ((string) null).Or("Hello").ShouldBe("Hello");

        #endregion
    }
}