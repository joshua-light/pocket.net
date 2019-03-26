using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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
    public CSharp Text(string text, bool when) =>
      With(_code.Text(text, when));
    
    public CSharp NewLine() =>
      With(_code.NewLine());
    public CSharp NewLine(bool when) =>
      With(_code.NewLine(when));

    private CSharp With(Code _) => this;
    
    private CSharp Separate(IReadOnlyList<string> items, string with, bool andNewLine = false)
    {
      foreach (var item in items)
        Text(item).Text(with, when: item != items.Last()).NewLine(when: andNewLine);

      return this;
    }

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

    public CSharp Field(FieldInfo field)
    {
      var type = field.FieldType.PrettyName(context: field.DeclaringType);
      
      return Text($"{Attributes(field)}{Modifier()} {type} {field.Name};");
      
      string Modifier() =>
        field.IsPublic ? "public" : field.IsPrivate ? "private" : "protected";
    }

    public CSharp Property(PropertyInfo property)
    {
      return Text($"{Attributes(property)}{Modifier()} " +
                  $"{property.PropertyType.PrettyName(context: property.DeclaringType)} {property.Name} " +
                  $"{{ {Body(property.GetMethod, "get")}{Body(property.SetMethod, "set")}}}");

      string Modifier() =>
            ModifierOf(property.GetMethod).Order > ModifierOf(property.SetMethod).Order
          ? ModifierOf(property.GetMethod).Text
          : ModifierOf(property.SetMethod).Text;

      string Body(MethodInfo method, string text) =>
        method != null
          ? ModifierOf(method).Text.As(x => x == Modifier() ? $"{text}; " : $"{x} {text}; ")
          : "";

      (int Order, string Text) ModifierOf(MethodInfo method) =>
        method == null
          ? (1, "private")
          : method.IsPublic ? (3, "public") : method.IsPrivate ? (1, "private") : (2, "protected");
    }
    
    public Code.Scope Declaration(Type type, bool partial = false)
    {
      return Text($"{Modifier()}{Partial()} {Kind()} {type.PrettyName()}{Parent()}").NewLine().Scope();
      
      string Modifier() =>
        (type.IsNested ? type.IsNestedPublic : type.IsPublic)
          ? "public"
          : "private";

      string Partial() =>
        partial ? " partial" : "";

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
        if (type.BaseType == null || type.BaseType == typeof(object))
          return "";

        var name = type.BaseType.IsNested
          ? type.BaseType.PrettyName(context: type)
          : type.BaseType.PrettyName();

        return $" : {name}";
      }
    }
    
    public CSharp Enum(Type type)
    {
      using (Declaration(type))
      {
        var mappings = Mappings().Select(x => $"{x.Name} = {x.Value}").ToList();

        Separate(mappings, with: ",", andNewLine: true);
      }

      IReadOnlyList<(string Name, object Value)> Mappings() =>
        type.Enum().Values;
      
      return this;
    }

    public struct MethodArguments
    {
      public struct One
      {
        public One(string value) : this(null, value) { }
        public One(string name, string value)
        {
          Name = name;
          Value = value;
        }
        
        public readonly string Name;
        public readonly string Value;

        public override string ToString() =>
          Name.IsNullOrEmpty() ? Value : $"{Name}: {Value}";
      }
      
      private readonly List<One> _all;

      public MethodArguments(List<One> all) =>
        _all = all;

      public MethodArguments Arg(string value) =>
        With(new One(value));
      public MethodArguments Arg(string name, string value) =>
        With(new One(name, value));

      public MethodArguments As<TEnum>() where TEnum : Enum
      {
        _all[_all.Count - 1] = new One(_all.Last().Name, $"{typeof(TEnum).Name}.{_all.Last().Value}");

        return this;
      }
        
      public override string ToString() =>
        _all.Separated(with: ", ");

      private MethodArguments With(One arg)
      {
        _all.Add(arg);

        return this;
      }
    }

    public CSharp Method(string name, Func<MethodArguments, MethodArguments> args = null)
    {
      var argsText = args == null ? "" : args(new MethodArguments(new List<MethodArguments.One>())).ToString();
      
      return Text($"{name}({argsText});");
    }

    private static string Attributes(MemberInfo member)
    {
      var joined = member
        .GetCustomAttributesData()
        .Select(Attribute)
        .Separated(with: " ");
      if (joined.IsEmpty())
        return "";

      return $"{joined} ";
    }

    private static string Attribute(CustomAttributeData attribute)
    {
      var name = attribute.AttributeType.PrettyName().Without("Attribute").AtEnd;
      var namedArguments = attribute.NamedArguments;
      var ctorArguments = attribute.ConstructorArguments;
      if (ctorArguments.IsEmpty() && namedArguments.IsEmpty())
        return $"[{name}]";

      var arguments = ctorArguments.Select(x => x.ToString())
        .Concat(namedArguments.Reverse().Select(x => x.ToString()))
        .Separated(with: ", ");
      
      return $"[{name}({arguments})]";
    }
  }
}