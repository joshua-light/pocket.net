using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.System.Text.Parts.Extensions
{
    public class StringPartsExtensionsTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Oscar", "Wilde")]
        [InlineData("Vladimir", "Nabokov")]
        [InlineData("Hermann", "Hesse")]
        public void With_ShouldReturnTwoStringParts_ThatRepresentConcatenatedStrings(string a, string b)
        {
            string c = a.With(b);

            c.ShouldBe(a + b);
        }
    }
}