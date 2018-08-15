using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class GenericExtensionsTest
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
    }
}