using System;
using System.Collections.Generic;
using System.Text;

namespace Pocket.Common
{
    public class Code
    {
        private interface IText
        {
            Code Write(string text);
            Code WriteLine(string text);
        }

        private class StringBuilderText : IText
        {
            private readonly Code _code;
            private readonly StringBuilder _sb;

            public StringBuilderText(Code code)
            {
                _code = code;
                _sb = new StringBuilder();
            }

            public override string ToString() => _sb.ToString();

            public Code Write(string text) =>
                With(_sb.Append(text));
            public Code WriteLine(string text) =>
                With(_sb.AppendLine(text));
            
            private Code With(StringBuilder _) => _code;
        }

        private class IndentedText : IText
        {
            private static class Cached
            {
                private static readonly Dictionary<int, string> Cache =
                    new Dictionary<int, string>();

                public static string String(int with) =>
                    Cache.One(with, () => new string(' ', with));
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

            public Code Write(string text)
            {
                if (_newLine.Use())
                    _text.Write(Cached.String(with: _indent));

                return _text.Write(text);
            }

            public Code WriteLine(string text)
            {
                if (_newLine.UseAndReset())
                    _text.Write(Cached.String(with: _indent));

                return _text.WriteLine(text);
            }
        }
        
        public struct IndentScope : IDisposable
        {
            private readonly Code _code;
            private readonly IText _oldText;

            public IndentScope(Code code, int size)
            {
                _code = code;
                _oldText = code._text;
                _code._text = new IndentedText(code._text, size);
            }
            
            public void Dispose() => _code._text = _oldText;
        }
        
        private IText _text;
        
        public Code()
        {
            _text = new StringBuilderText(this);
        }

        public string Text => _text.ToString();

        public Code Write(string text) =>
            _text.Write(text);
        public Code WriteLine(string text) =>
            _text.WriteLine(text);

        public IndentScope Indent(int size) =>
           new IndentScope(this, size);
    }
}