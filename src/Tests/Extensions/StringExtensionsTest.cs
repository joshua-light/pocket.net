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

        #region Map

        [Fact]
        public void MapOfCharToString_ShouldReplaceEveryCharacterWithOther() =>
            "1234".Map(x => '*').ShouldBe("****");

        [Fact]
        public void MapOfCharToString_ShouldReplaceEveryCharacterWithString() =>
            "1234".Map(x => "**").ShouldBe("********");

        #endregion

        #region Without

        [Theory]
        [InlineData("Test", "t", "Tes")]
        [InlineData("Test", "st", "Te")]
        [InlineData("Test", "est", "T")]
        [InlineData("Test", "Test", "")]
        
        [InlineData("Test", "te", "Test")]
        [InlineData("Test", "123", "Test")]
        [InlineData("Test", "fasd", "Test")]
        public void WithoutPartAtEnd_ShouldWorkCorrectly(string source, string part, string expected) =>
            source.Without(part).AtEnd.ShouldBe(expected);
        
        [Theory]
        [InlineData("Test", "T", "est")]
        [InlineData("Test", "Te", "st")]
        [InlineData("Test", "Tes", "t")]
        [InlineData("Test", "Test", "")]
        
        [InlineData("Test", "te", "Test")]
        [InlineData("Test", "123", "Test")]
        [InlineData("Test", "fasd", "Test")]
        public void WithoutPartAtStart_ShouldWorkCorrectly(string source, string part, string expected) =>
            source.Without(part).AtStart.ShouldBe(expected);

        [Theory]
        [InlineData("Test", "es", "Tt")]
        [InlineData("TestTest", "es", "TtTt")]
        [InlineData("TestTest", "Test", "")]
        public void WithoutEverywhere_ShouldReplacePartWithEmptyString(string source, string part, string expected) =>
            source.Without(part).Everywhere.ShouldBe(expected);

        #endregion
    }
}