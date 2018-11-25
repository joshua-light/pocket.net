using System.Text;

namespace Pocket.Common
{
    public struct StringParts
    {
        private readonly StringBuilder _string;
        
        internal StringParts(string a, string b, string c, string d, string e)
        {
            _string = new StringBuilder()
                .Append(a)
                .Append(b)
                .Append(c)
                .Append(d)
                .Append(e);
        }

        public StringParts With(string part)
        {
            _string.Append(part);

            return this;
        }

        public static implicit operator string(StringParts self) =>
            self._string.ToString();
    }
}