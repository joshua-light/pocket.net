using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.System.Text.Parts
{
    public class ThreeStringPartsTest
    {
        [Theory]
        [InlineData("", "", "")]
        [InlineData("Oscar", "Fingal", "Wilde")]
        [InlineData("Vladimir", "Vladimirovich", "Nabokov")]
        [InlineData("Hermann", "Karl", "Hesse")]
        public void ImplicitCastToString_ShouldReturnConcatenatedString(string a, string b, string c)
        {
            string d = new ThreeStringParts(a, b, c);
            
            d.ShouldBe(a + b + c);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("Oscar", "Fingal", "Wilde")]
        [InlineData("Vladimir", "Vladimirovich", "Nabokov")]
        [InlineData("Hermann", "Karl", "Hesse")]
        public void ToString_ShouldReturnConcatenatedString(string a, string b, string c) =>
            new ThreeStringParts(a, b, c).ToString().ShouldBe(a + b + c);
    }
}