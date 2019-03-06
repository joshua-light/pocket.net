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

        public struct Scope : IDisposable
        {
            private readonly Code _code;
            private readonly Action<Code> _end;

            public Scope(Code code, Action<Code> begin, Action<Code> end)
            {
                _code = code;
                _end = end;

                begin(code);
            }

            public void Dispose() => _end(_code);
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

        public Scope Indent(int size)
        {
            var oldText = _text;

            return new Scope(this, x => x._text = new IndentedText(x._text, size), x => x._text = oldText);
        }
    }
}