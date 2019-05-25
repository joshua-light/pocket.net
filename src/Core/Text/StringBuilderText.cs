using System.Text;

namespace Pocket.Common
{
  public class StringBuilderText : IText
  {
    private readonly StringBuilder _sb;
    
    public StringBuilderText() : this(new StringBuilder()) { }
    public StringBuilderText(StringBuilder sb) =>
      _sb = sb;

    public override string ToString() =>
      _sb.ToString();

    public bool IsEmpty => _sb.Length == 0;

    public IText With(string text) =>
      With(_sb.Append(text));
            
    public IText NewLine() =>
      With(_sb.AppendLine());
            
    private IText With(StringBuilder _) => this;
  }
}