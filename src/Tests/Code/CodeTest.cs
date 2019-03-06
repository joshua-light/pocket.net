using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Code
{
    public class CodeTest
    {
        [Fact]
        public void ToString_OfEmptyCode_ShouldBeEmptyString() =>
            Code().ToString().ShouldBe("");

        [Fact]
        public void Write_ShouldChangeText() =>
            Code().Text("Hello").ToString().ShouldBe("Hello");

        [Fact]
        public void Write_AfterWriteLine_ShouldStartAtNewLine() =>
            Code().Text("").NewLine().Text("Hello").ToString().ShouldBe(@"
Hello");

        [Fact]
        public void UsingIndent_ShouldAddSpacesForEveryNewLine()
        {
            var code = Code();

            using (code.Indent(size: 4))
            {
                code.Text("1");
                code.Text("2");
                code.Text("3");
                code.Text("4");
            }
            
            code.ToString().ShouldBe(@"    1
    2
    3
    4");
        }
        
        [Fact]
        public void UsingDoubleIndent_ShouldAddSpacesForEveryNewLine()
        {
            var code = Code();

            using (code.Indent(size: 4))
            {
                code.Text("1").NewLine();
                
                using (code.Indent(size: 2))
                    code.Text("2").NewLine();
                
                code.Text("3");
            }
            
            code.ToString().ShouldBe(@"    1
      2
    3");
        }
        
        private static Common.Code Code() => new Common.Code();
    }
}