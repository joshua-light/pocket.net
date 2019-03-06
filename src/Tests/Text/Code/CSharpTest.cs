using System.Collections.Generic;
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

    [Fact]
    public void Namespace_ShouldAppendNamespaceScopeWithoutNewLine()
    {
      var code = CSharp();

      using (code.Namespace("Test"))
        code.Text("Hello").NewLine();

      code.ToString().ShouldBe(@"namespace Test
{
    Hello
}");
    }

    [Fact]
    public void Using_ShouldAppendUsingStatementWithSpecifiedNamespaceAndSemicolon() =>
      CSharp().Using("System").ToString().ShouldBe("using System;");
    
    [Fact]
    public void UsingWithType_ShouldAppendUsingStatementWithSpecifiedNamespaceAndSemicolon() =>
      CSharp().Using(typeof(List<>)).ToString().ShouldBe("using System.Collections.Generic;");

    [Fact]
    public void Class_ShouldAppendScopeWithClassHeader_IfClassIsPrivate()
    {
      var code = CSharp();
      
      using (code.Class(typeof(PrivateClass))) { }
      
      code.ToString().ShouldBe(@"private class PrivateClass
{
}
");
    }
    
    [Fact]
    public void Class_ShouldAppendScopeWithClassHeader_IfClassIsPublic()
    {
      var code = CSharp();
      
      using (code.Class(typeof(PublicClass))) { }
      
      code.ToString().ShouldBe(@"public class PublicClass
{
}
");
    }
    
    [Fact]
    public void Class_ShouldAppendScopeWithClassHeader_IfClassHasParent()
    {
      var code = CSharp();
      
      using (code.Class(typeof(Class))) { }
      
      code.ToString().ShouldBe(@"public class Class : BaseClass
{
}
");
    }
    
    private static CSharp CSharp() => new Common.Code().CSharp();

    #region Nested Types

    private class PrivateClass { } 
    public class PublicClass { }
    
    public class BaseClass { }
    public class Class : BaseClass { }

    #endregion
  }
}