using System.Collections.Generic;

namespace Pocket.Common
{
  public class IndentedText : IText
  {
    private static readonly Dictionary<int, string> Cache = new Dictionary<int, string>();
    
    private static string Blank(int ofSize) =>
      Cache.One(ofSize, or: () => new string(' ', ofSize));
            
    private readonly IText _text;
    private readonly int _indent;
    private readonly BoolToggle _newLine;

    public IndentedText(IText text, int indent)
    {
      _text = text;
      _indent = indent;
      _newLine = new BoolToggle(true, or: false);
    }

    public IText With(string text) => _text
      .With(Blank(ofSize: _indent), when: _newLine)
      .With(text);
          
    public IText NewLine()
    {
      _newLine.Reset();

      return _text.NewLine();
    }
  }
}