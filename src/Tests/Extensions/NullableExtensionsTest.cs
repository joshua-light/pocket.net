using Shouldly;
using Xunit;

namespace Pocket.Tests.Extensions
{
    public class NullableExtensionsTest
    {
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("Hello")]
        [InlineData("World")]
        public void Or_ShouldReturnSelf_IfItIsNotNull(string value) =>
            value.Or("1").ShouldBe(value);

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("Hello")]
        [InlineData("World")]
        public void Or_ShouldReturnDefaultValue_IfSelfIsNull(string def) =>
            ((string) null).Or(def).ShouldBe(def);

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void OrDefault_ShouldReturnSelf_IfItIsNotNull(int value) =>
            new int?(value).OrDefault().ShouldBe(value);


        [Fact]
        public void OrDefault_ShouldReturnDefaultValue_IfSelfIsNull() =>
            ((int?) null).OrDefault().ShouldBe(0);
    }
}