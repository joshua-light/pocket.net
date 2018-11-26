using System.Runtime.CompilerServices;
using System.Text;

namespace Pocket.Common
{
    public struct StringParts
    {
        private StringBuilder _string;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal StringParts(string a, string b, string c, string d, string e)
        {
            _string = new StringBuilder()
                .Append(a)
                .Append(b)
                .Append(c)
                .Append(d)
                .Append(e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringParts With(string part)
        {
            _string = _string ?? new StringBuilder();
            _string.Append(part);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator string(StringParts self) =>
            self._string?.ToString() ?? "";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() =>
            this;
    }
}