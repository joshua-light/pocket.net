using System;
using Shouldly;
using Xunit;

namespace Pocket.Tests.Text.Code.Core
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
            Code().Text("").NewLine().Text("Hello").ToString().ShouldBe(
                "" + Environment.NewLine +
                "Hello");

        [Fact]
        public void UsingIndent_ShouldAddSpacesForEveryNewLine()
        {
            var code = Code();

            using (code.Indent(size: 4))
            {
                code.Text("1").NewLine();
                code.Text("2").NewLine();
                code.Text("3").NewLine();
                code.Text("4");
            }
            
            code.ToString().ShouldBe(
                "    1" + Environment.NewLine +
                "    2" + Environment.NewLine +
                "    3" + Environment.NewLine +
                "    4");
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

            code.ToString().ShouldBe(
                "    1"   + Environment.NewLine +              
                "      2" + Environment.NewLine + 
                "    3");
        }

        private static Pocket.Code Code() => new Pocket.Code();
    }
}