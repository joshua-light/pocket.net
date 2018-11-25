using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.System.Text.Parts
{
    public class StringPartsTest
    {
        [Theory]
        [InlineData("", "", "", "", "")]
        [InlineData("Oscar", "Fingal", "O'Flahertie", "Wills", "Wilde")]
        public void ImplicitCastToString_ShouldReturnConcatenatedString(string a, string b, string c, string d, string e)
        {
            string f = new StringParts(a, b, c, d, e);
            
            f.ShouldBe(a + b + c + d + e);
        }
        
        [Theory]
        [InlineData("", "", "", "", "")]
        [InlineData("Oscar", "Fingal", "O'Flahertie", "Wills", "Wilde")]
        public void With_ShouldAddSpecifiedStringToOtherParts(string a, string b, string c, string d, string e) =>
            new StringParts()
                .With(a)
                .With(b)
                .With(c)
                .With(d)
                .With(e)
                .ToString()
                .ShouldBe(a + b + c + d + e);

        [Theory]
        [InlineData("", "", "", "", "")]
        [InlineData("Oscar", "Fingal", "O'Flahertie", "Wills", "Wilde")]
        public void ToString_ShouldReturnConcatenatedString(string a, string b, string c, string d, string e) =>
            new StringParts(a, b, c, d, e).ToString().ShouldBe(a + b + c + d + e);
    }
}