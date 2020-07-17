using System.Collections.Generic;

namespace Pocket
{
  public class IndentedText : IText
  {
    private static readonly Dictionary<int, string> Cache = new Dictionary<int, string>();

    private static string Blank(int ofSize) =>
      Cache.One(ofSize).OrNew(() => new string(' ', ofSize));
            
    private readonly IText _text;
    private readonly int _indent;
    
    private bool _newLine;

    public IndentedText(IText text, int indent)
    {
      _text = text;
      _indent = indent;
      _newLine = true;
    }

    public bool IsEmpty => _text.IsEmpty;

    public IText With(string text) => _text
      .With(Blank(ofSize: _indent), when: Is(ref _newLine))
      .With(text);
          
    public IText NewLine()
    {
      _newLine = true;

      return _text.NewLine();
    }

    private static bool Is(ref bool newLine) =>
      newLine ? !(newLine = false) : false;
  }
}