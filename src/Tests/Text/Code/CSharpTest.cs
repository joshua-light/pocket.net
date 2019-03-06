using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Text.Code
{
  public class CSharpTest
  {
    [Fact]
    public void Scope_ShouldAppendStartAndEndBrackets_AndUseIndent()
    {
      var code = CSharp();

      using (code.Scope())
        code.Text("Hello").NewLine();
      
      code.ToString().ShouldBe(@"{
    Hello
}
");
    }
    
    [Fact]
    public void ScopeThatNotEndsWithNewLine_ShouldNotAppendNewLineAtEnd()
    {
      var code = CSharp();

      using (code.Scope(endsWithNewLine: false))
        code.Text("Hello").NewLine();
      
      code.ToString().ShouldBe(@"{
    Hello
}");
    }

    [Fact]
    public void ScopeWithHeader_ShouldAppendHeaderAndThenScopeAtNewLine()
    {
      var code = CSharp();

      using (code.Scope(header: "namespace Test"))
        code.Text("Hello").NewLine();
      
      code.ToString().ShouldBe(@"namespace Test
{
    Hello
}
");
    }

    [Fact]
    public void Region_ShouldAppendStartAndEndRegionText_AndNotChangeIndent()
    {
      var code = CSharp();

      using (code.Scope())
      using (code.Region("Test"))
        code.Text("Hello").NewLine();

      code.ToString().ShouldBe(@"{
    #region Test
    Hello
    #endregion
}
");
    }
    
    private static CSharp CSharp() => new Common.Code().CSharp();
  }
}