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
      
      code.ToString().ShouldBe(
        "{"         + Environment.NewLine + 
        "    Hello" + Environment.NewLine + 
        "}"         + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void ScopeThatNotEndsWithNewLine_ShouldNotAppendNewLineAtEnd()
    {
      var code = CSharp();

      using (code.Scope(endsWithNewLine: false))
        code.Text("Hello").NewLine();
      
      code.ToString().ShouldBe(
        "{"         + Environment.NewLine + 
        "    Hello" + Environment.NewLine + 
        "}");
    }

    [Fact]
    public void ScopeWithHeader_ShouldAppendHeaderAndThenScopeAtNewLine()
    {
      var code = CSharp();

      using (code.Scope(header: "namespace Test"))
        code.Text("Hello").NewLine();
      
      code.ToString().ShouldBe(
        "namespace Test" + Environment.NewLine + 
        "{"              + Environment.NewLine + 
        "    Hello"      + Environment.NewLine + 
        "}"              + Environment.NewLine + 
        "");
    }

    [Fact]
    public void Region_ShouldAppendStartAndEndRegionText_AndNotChangeIndent()
    {
      var code = CSharp();

      using (code.Scope())
      using (code.Region("Test"))
        code.Text("Hello").NewLine();

      code.ToString().ShouldBe(
        "{" +                Environment.NewLine + 
        "    #region Test" + Environment.NewLine + 
        "    Hello" +        Environment.NewLine + 
        "    #endregion" +   Environment.NewLine + 
        "}" +                Environment.NewLine + 
        "");
    }

    [Fact]
    public void Namespace_ShouldAppendNamespaceScopeWithoutNewLine()
    {
      var code = CSharp();

      using (code.Namespace("Test"))
        code.Text("Hello").NewLine();
      
      code.ToString().ShouldBe(
        "namespace Test" + Environment.NewLine + 
        "{"              + Environment.NewLine + 
        "    Hello"      + Environment.NewLine + 
        "}");
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
      
      code.ToString().ShouldBe(
        "private class PrivateClass" + Environment.NewLine + 
        "{"                          + Environment.NewLine + 
        "}"                          + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassIsPublic()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicClass))) { }
      
      code.ToString().ShouldBe(
        "public class PublicClass"   + Environment.NewLine + 
        "{"                          + Environment.NewLine + 
        "}"                          + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassIsPartial()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PartialClass), partial: true)) { }
      
      code.ToString().ShouldBe(
        "public partial class PartialClass" + Environment.NewLine + 
        "{"                                 + Environment.NewLine + 
        "}"                                 + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassHasParent()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(Class))) { }
      
      code.ToString().ShouldBe(
        "public class Class : BaseClass" + Environment.NewLine + 
        "{"                              + Environment.NewLine + 
        "}"                              + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassHasParentAndParentIsNested()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(Class2))) { }
      
      code.ToString().ShouldBe(
        "public class Class2 : BaseClass.Nested" + Environment.NewLine + 
        "{"                                      + Environment.NewLine + 
        "}"                                      + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithClassHeader_IfClassHasParentAndParentIsOuterNested()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(OuterClass))) { }
      
      code.ToString().ShouldBe(
        "public class OuterClass : OuterClassWith.Nested.One" + Environment.NewLine + 
        "{"                                                   + Environment.NewLine + 
        "}"                                                   + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithStructHeader()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicStruct))) { }
      
      code.ToString().ShouldBe(
        "public struct PublicStruct"   + Environment.NewLine + 
        "{"                            + Environment.NewLine + 
        "}"                            + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithEnumHeader()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicEnum))) { }
      
      code.ToString().ShouldBe(
        "public enum PublicEnum"   + Environment.NewLine + 
        "{"                        + Environment.NewLine + 
        "}"                        + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Declaration_ShouldAppendScopeWithEnumHeader_IfEnumHasNotIntBaseType()
    {
      var code = CSharp();
      
      using (code.Declaration(typeof(PublicLongEnum))) { }
      
      code.ToString().ShouldBe(
        "public enum PublicLongEnum : long"   + Environment.NewLine + 
        "{"                                   + Environment.NewLine + 
        "}"                                   + Environment.NewLine + 
        "");
    }
    
    [Fact]
    public void Enum_ShouldAppendScopeWithEnumHeaderAndAllValues() =>
      CSharp().Enum(typeof(EnumWithValues)).ToString().ShouldBe(
        "public enum EnumWithValues" + Environment.NewLine + 
        "{"                          + Environment.NewLine + 
        "    A = 1,"                 + Environment.NewLine + 
        "    B = 2,"                 + Environment.NewLine + 
        "    C = 3,"                 + Environment.NewLine + 
        "    D = 4"                  + Environment.NewLine + 
        "}"                          + Environment.NewLine + 
        "");

    #region Field

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
    public void Field_ShouldAppendCode_IfFieldHasAttributeWithValueInCtor() =>
      Field(typeof(ClassWithFields), withName: "FieldWithAttributeWithCtorArgument")
        .ToString()
        .ShouldBe("[XmlAttribute(\"Test\")] public List<int> FieldWithAttributeWithCtorArgument;");
    
    [Fact]
    public void Field_ShouldAppendCode_IfFieldHasAttributeWithValuesInCtor() =>
      Field(typeof(ClassWithFields), withName: "FieldWithAttributeWithCtorArguments")
        .ToString()
        .ShouldBe("[XmlAttribute(\"Test\", typeof(System.Int32))] public List<int> FieldWithAttributeWithCtorArguments;");

    [Fact]
    public void Field_ShouldAppendCode_IfFieldHasAttributeWithProperty() =>
      Field(typeof(ClassWithFields), withName: "FieldWithAttributeWithProperty")
        .ToString()
        .ShouldBe("[XmlAttribute(AttributeName = \"Test\")] public List<int> FieldWithAttributeWithProperty;");
    
    [Fact]
    public void Field_ShouldAppendCode_IfFieldHasAttributeWithProperties() =>
      Field(typeof(ClassWithFields), withName: "FieldWithAttributeWithProperties")
        .ToString()
        .ShouldBe("[XmlAttribute(AttributeName = \"Test\", Type = typeof(System.Int32))] public List<int> FieldWithAttributeWithProperties;");
    
    [Fact]
    public void Field_ShouldAppendCode_IfFieldHasAttributeWithCtorArgumentAndProperty() =>
      Field(typeof(ClassWithFields), withName: "FieldWithCtorArgumentAndProperty")
        .ToString()
        .ShouldBe("[XmlAttribute(\"Test\", Type = typeof(System.Int32))] public List<int> FieldWithCtorArgumentAndProperty;");
    
    [Fact]
    public void Field_ShouldAppendCode_IfFieldIsOfNestedType() =>
      Field(typeof(ClassWithFields), withName: "FieldOfNestedType")
        .ToString()
        .ShouldBe("public Nested FieldOfNestedType;");

    #endregion
    
    #region Property

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

    #endregion

    private static CSharp CSharp() => new Common.Code().CSharp();

    private static CSharp Field(Type type, string withName) =>
      CSharp().Field(type.Fields(_ => _.AllStatic().And.AllInstance()).First(x => x.Name == withName));

    private static CSharp Property(Type type, string withName) =>
      CSharp().Property(type.Properties(_ => _.AllStatic().And.AllInstance()).First(x => x.Name == withName));

    #region Nested Types

    private class PrivateClass { }
    public class PublicClass { }
    public partial class PartialClass { }
    public partial class PartialClass { }
    
    public class BaseClass { public class Nested { } }
    public class Class : BaseClass { }
    public class Class2 : BaseClass.Nested { }
    
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
      public class Nested { }
      
      private int _privateField;
      protected long ProtectedField;
      public string PublicField;

      [XmlAttribute] [SoapAttribute] public List<int> FieldWithAttributes;
      
      [XmlAttribute("Test")] public List<int> FieldWithAttributeWithCtorArgument;
      [XmlAttribute("Test", typeof(int))] public List<int> FieldWithAttributeWithCtorArguments;
      
      [XmlAttribute(AttributeName = "Test")] public List<int> FieldWithAttributeWithProperty;
      [XmlAttribute(AttributeName = "Test", Type = typeof(int))] public List<int> FieldWithAttributeWithProperties;
      
      [XmlAttribute("Test", Type = typeof(int))] public List<int> FieldWithCtorArgumentAndProperty;

      public Nested FieldOfNestedType;
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

  public class OuterClass : OuterClassWith.Nested.One { }
  public class OuterClassWith { public class Nested { public class One { } } }
}