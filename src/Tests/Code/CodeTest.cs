using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Code
{
    public class CodeTest
    {
        [Fact]
        public void Text_OfEmptyCode_ShouldBeEmptyString() =>
            Code().Text.ShouldBe("");

        [Fact]
        public void Write_ShouldChangeText() =>
            Code().Write("Hello").Text.ShouldBe("Hello");

        [Fact]
        public void Write_AfterWriteLine_ShouldStartAtNewLine() =>
            Code().WriteLine("").Write("Hello").Text.ShouldBe(@"
Hello");

        [Fact]
        public void UsingIndent_ShouldAddSpacesForEveryNewLine()
        {
            var code = Code();

            using (code.Indent(size: 4))
            {
                code.WriteLine("1");
                code.WriteLine("2");
                code.WriteLine("3");
                code.Write("4");
            }
            
            code.Text.ShouldBe(@"    1
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
                code.WriteLine("1");
                
                using (code.Indent(size: 2))
                    code.WriteLine("2");
                
                code.Write("3");
            }
            
            code.Text.ShouldBe(@"    1
      2
    3");
        }
        
        private static Common.Code Code() => new Common.Code();
    }
}