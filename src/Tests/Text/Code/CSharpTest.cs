using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
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
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassIsPrivate()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PrivateClass))) { }
      
      code.ToString().ShouldBe(@"private class PrivateClass
{
}
");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassIsPublic()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicClass))) { }
      
      code.ToString().ShouldBe(@"public class PublicClass
{
}
");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassHasParent()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(Class))) { }
      
      code.ToString().ShouldBe(@"public class Class : BaseClass
{
}
");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithStructHeader()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicStruct))) { }
      
      code.ToString().ShouldBe(@"public struct PublicStruct
{
}
");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithEnumHeader()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicEnum))) { }
      
      code.ToString().ShouldBe(@"public enum PublicEnum
{
}
");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithEnumHeader_IfEnumHasNotIntBaseType()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicLongEnum))) { }
      
      code.ToString().ShouldBe(@"public enum PublicLongEnum : long
{
}
");
    }
    
    [Fact]
    public void Enum_ShouldAppendScopeWithEnumHeaderAndAllValues() =>
      CSharp().Enum(typeof(EnumWithValues)).ToString().ShouldBe(@"public enum EnumWithValues
{
    A = 1,
    B = 2,
    C = 3,
    D = 4
}
");

    [Fact]
    public void Field_ShouldAppendCode_IfFieldIsPrivate() =>
      Field(typeof(ClassWithFields), withName: "_privateField")
        .ToString()
        .ShouldBe("private int _privateField;");
    
    [Fact]
    public void Field_ShouldAppendCode_IfFieldIsProtected() =>
      Field(typeof(ClassWithFields), withName: "ProtectedField")
        .ToString()
        .ShouldBe("protected long ProtectedField;");
    
    [Fact]
    public void Field_ShouldAppendCode_IfFieldIsPublic() =>
      Field(typeof(ClassWithFields), withName: "PublicField")
        .ToString()
        .ShouldBe("public string PublicField;");
    
    [Fact]
    public void Field_ShouldAppendCode_IfFieldHasAttributes() =>
      Field(typeof(ClassWithFields), withName: "FieldWithAttributes")
        .ToString()
        .ShouldBe("[XmlAttribute] [SoapAttribute] public List<int> FieldWithAttributes;");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsPrivateReadonly() =>
      Property(typeof(ClassWithProperties), withName: "PrivateProperty")
        .ToString()
        .ShouldBe("private int PrivateProperty { get; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsPrivate() =>
      Property(typeof(ClassWithProperties), withName: "PrivatePropertyWithSet")
        .ToString()
        .ShouldBe("private int PrivatePropertyWithSet { get; set; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsProtectedReadonly() =>
      Property(typeof(ClassWithProperties), withName: "ProtectedProperty")
        .ToString()
        .ShouldBe("protected int ProtectedProperty { get; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsProtected() =>
      Property(typeof(ClassWithProperties), withName: "ProtectedPropertyWithSet")
        .ToString()
        .ShouldBe("protected int ProtectedPropertyWithSet { get; set; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsProtectedWithPrivateGet() =>
      Property(typeof(ClassWithProperties), withName: "ProtectedPropertyWithPrivateGet")
        .ToString()
        .ShouldBe("protected int ProtectedPropertyWithPrivateGet { private get; set; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsProtectedWithPrivateSet() =>
      Property(typeof(ClassWithProperties), withName: "ProtectedPropertyWithPrivateSet")
        .ToString()
        .ShouldBe("protected int ProtectedPropertyWithPrivateSet { get; private set; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsPublicReadonly() =>
      Property(typeof(ClassWithProperties), withName: "PublicProperty")
        .ToString()
        .ShouldBe("public int PublicProperty { get; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsPublic() =>
      Property(typeof(ClassWithProperties), withName: "PublicPropertyWithSet")
        .ToString()
        .ShouldBe("public int PublicPropertyWithSet { get; set; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsPublicWithProtectedGet() =>
      Property(typeof(ClassWithProperties), withName: "PublicPropertyWithProtectedGet")
        .ToString()
        .ShouldBe("public int PublicPropertyWithProtectedGet { protected get; set; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfPropertyIsPublicWithProtectedSet() =>
      Property(typeof(ClassWithProperties), withName: "PublicPropertyWithProtectedSet")
        .ToString()
        .ShouldBe("public int PublicPropertyWithProtectedSet { get; protected set; }");
    
    [Fact]
    public void Property_ShouldAppendCode_IfItHasAttributes() =>
      Property(typeof(ClassWithProperties), withName: "PropertyWithAttributes")
        .ToString()
        .ShouldBe("[XmlAttribute] [SoapAttribute] public List<int> PropertyWithAttributes { get; }");
    
    private static CSharp CSharp() => new Common.Code().CSharp();

    private static CSharp Field(Type type, string withName) =>
      CSharp().Field(type.Fields(_ => _.AllStatic().And.AllInstance()).First(x => x.Name == withName));

    private static CSharp Property(Type type, string withName) =>
      CSharp().Property(type.Properties(_ => _.AllStatic().And.AllInstance()).First(x => x.Name == withName));

    #region Nested Types

    private class PrivateClass { }
    public class PublicClass { }
    
    public class BaseClass { }
    public class Class : BaseClass { }
    
    public struct PublicStruct { }
    public enum PublicEnum { }
    public enum PublicLongEnum : long { }

    public enum EnumWithValues
    {
      A = 1,
      B = 2,
      C = 3,
      D = 4,
    }

    public class ClassWithFields
    {
      private int _privateField;
      protected long ProtectedField;
      public string PublicField;

      [XmlAttribute] [SoapAttribute] public List<int> FieldWithAttributes;
    }
    
    public class ClassWithProperties
    {
      private int PrivateProperty { get; }
      private int PrivatePropertyWithSet { get; set; }
      
      protected int ProtectedProperty { get; }
      protected int ProtectedPropertyWithSet { get; set; }
      protected int ProtectedPropertyWithPrivateGet { private get; set; }
      protected int ProtectedPropertyWithPrivateSet { get; private set; }
      
      public int PublicProperty { get; }
      public int PublicPropertyWithSet { get; set; }
      public int PublicPropertyWithProtectedGet { protected get; set; }
      public int PublicPropertyWithProtectedSet { get; protected set; }

      [XmlAttribute] [SoapAttribute] public List<int> PropertyWithAttributes { get; }
    }

    #endregion
  }
}