using System;
using System.Collections.Generic;
using System.Text;

namespace Pocket.Common
{
    public class Code
    {
        private interface IText
        {
            Code With(string text);
            Code NewLine();
        }

        private class StringBuilderText : IText
        {
            private readonly Code _code;
            private readonly StringBuilder _sb = new StringBuilder();

            public StringBuilderText(Code code) =>
                _code = code;

            public override string ToString() => _sb.ToString();

            public Code With(string text) =>
                With(_sb.Append(text));
            
            public Code NewLine() =>
                With(_sb.AppendLine());
            
            private Code With(StringBuilder _) => _code;
        }

        private class IndentedText : IText
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

            public Code With(string text)
            {
                if (_newLine.Use())
                    _text.With(Cached.String(with: _indent));

                return _text.With(text);
            }

            public Code NewLine()
            {
                _newLine.Reset();

                return _text.NewLine();
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

        public override string ToString() =>
            _text.ToString();

        public Code Text(string text) =>
            _text.With(text);
        
        public Code NewLine() =>
            _text.NewLine();

        public Scope Indent(int size)
        {
            var oldText = _text;

            return new Scope(this,
                x => x._text = new IndentedText(x._text, size),
                x => x._text = oldText);
        }
    }
}