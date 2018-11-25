using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.System.Text.Parts
{
    public class FourStringPartsTest
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("Oscar", "Fingal", "O'Flahertie", "Wilde")]
        public void ImplicitCastToString_ShouldReturnConcatenatedString(string a, string b, string c, string d)
        {
            string e = new FourStringParts(a, b, c, d);
            
            e.ShouldBe(a + b + c + d);
        }

        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("Oscar", "Fingal", "O'Flahertie", "Wilde")]
        public void ToString_ShouldReturnConcatenatedString(string a, string b, string c, string d) =>
            new FourStringParts(a, b, c, d).ToString().ShouldBe(a + b + c + d);
    }
}