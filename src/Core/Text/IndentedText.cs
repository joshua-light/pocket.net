using System.Collections.Generic;

namespace Pocket.Common
{
  public class IndentedText : IText
  {
    private static class Cached
    {
      private static readonly Dictionary<int, string> Cache = new Dictionary<int, string>();

      public static string String(int with) =>
        Cache.One(with, or: () => new string(' ', with));
    }
            
    private readonly IText _text;
    private readonly int _indent;
    private readonly Toggle<bool> _newLine;

    public IndentedText(IText text, int indent)
    {
      _text = text;
      _indent = indent;
      _newLine = new Toggle<bool>(true, or: false);
    }

    public IText With(string text) => _text
      .With(Cached.String(with: _indent), when: _newLine.Current)
      .With(text);
          
    public IText NewLine()
    {
      _newLine.Reset();

      return _text.NewLine();
    }
  }
}