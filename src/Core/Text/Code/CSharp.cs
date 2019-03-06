using System;

namespace Pocket.Common
{
  public struct CSharp
  {
    private readonly Code _code;
    private readonly int _indent;

    public CSharp(Code code, int indent)
    {
      _code = code;
      _indent = indent;
    }

    public override string ToString() => _code.ToString();

    public CSharp Text(string text) =>
      With(_code.Text(text));
    public CSharp NewLine() =>
      With(_code.NewLine());

    public Code.Scope Scope(bool endsWithNewLine = true) => new Code.Scope(_code,
      x => x.Text("{").NewLine(),
      x => x.Text("}").NewLine(when: endsWithNewLine)).With(_code.Indent(_indent));

    public Code.Scope Scope(string header, bool endsWithNewLine = true) =>
      Text(header).NewLine().Scope(endsWithNewLine);
    
    public Code.Scope Region(string name) => new Code.Scope(_code,
      x => x.Text($"#region {name}").NewLine(),
      x => x.Text($"#endregion").NewLine());

    public Code.Scope Namespace(string name) =>
      Scope(header: $"namespace {name}", endsWithNewLine: false);

    public CSharp Using(Type namespaceOf) =>
      Using(namespaceOf.Namespace);
    public CSharp Using(string @namespace) =>
      Text($"using {@namespace};");

    public Code.Scope Declaration(Type type)
    {
      return Text($"{Modifier()} {Kind()} {type.PrettyName()}{Parent()}").NewLine().Scope();
      
      string Modifier() =>
        (type.IsNested ? type.IsNestedPublic : type.IsPublic)
          ? "public"
          : "private";

      string Kind() =>
        type.IsValueType ? type.IsEnum ? "enum" : "struct" : "class";

      string Parent()
      {
        if (type.IsEnum)
        {
          var underlying = type.GetEnumUnderlyingType();
          if (underlying != typeof(int))
            return $" : {underlying.PrettyName()}";

          return "";
        }
        
        if (type.IsValueType)
          return "";
        
        return type.BaseType != null && type.BaseType != typeof(object)
          ? $" : {type.BaseType.PrettyName()}"
          : "";
      }
    }

    private CSharp With(Code _) => this;
  }
}