using System;

namespace Pocket.Common
{
    public class Code
    {
        public struct Scope : IDisposable
        {
            private readonly Code _code;
            private Action<Code> _end;

            public Scope(Code code, Action<Code> begin, Action<Code> end)
            {
                _code = code;
                _end = end;

                begin(code);
            }

            public void Dispose() =>
                _end(_code);

            public Scope With(Scope other)
            {
                var end = _end;
                
                other._end += end;

                _end = other._end;
                
                return this;
            }
        }
        
        private IText _text = new StringBuilderText();

        public override string ToString() =>
            _text.ToString();

        public Code Text(string text) =>
            With(_text.With(text));
        public Code NewLine() =>
            With(_text.NewLine());

        public Scope Indent(int size)
        {
            var oldText = _text;

            return new Scope(this,
                x => x._text = new IndentedText(x._text, size),
                x => x._text = oldText);
        }

        private Code With(IText _) => this;
    }
}