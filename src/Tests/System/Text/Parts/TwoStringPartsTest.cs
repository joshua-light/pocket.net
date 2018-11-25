using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.System.Text.Parts
{
    public class TwoStringPartsTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Oscar", "Wilde")]
        [InlineData("Vladimir", "Nabokov")]
        [InlineData("Hermann", "Hesse")]
        public void ImplicitCastToString_ShouldReturnConcatenatedString(string a, string b)
        {
            string c = new TwoStringParts(a, b);
            
            c.ShouldBe(a + b);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Oscar", "Wilde")]
        [InlineData("Vladimir", "Nabokov")]
        [InlineData("Hermann", "Hesse")]
        public void ToString_ShouldReturnConcatenatedString(string a, string b) =>
            new TwoStringParts(a, b).ToString().ShouldBe(a + b);
    }
}