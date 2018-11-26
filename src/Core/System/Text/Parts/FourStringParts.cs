using System.Runtime.CompilerServices;

namespace Pocket.Common
{
    public struct FourStringParts
    {
        private readonly string _a;
        private readonly string _b;
        private readonly string _c;
        private readonly string _d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FourStringParts(string a, string b, string c, string d)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringParts With(string part) =>
            new StringParts(_a, _b, _c, _d, part);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator string(FourStringParts self) =>
            self._a + self._b + self._c + self._d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() =>
            this;
    }
}