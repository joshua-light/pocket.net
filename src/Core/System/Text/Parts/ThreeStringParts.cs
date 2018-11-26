using System.Runtime.CompilerServices;

namespace Pocket.Common
{
    public struct ThreeStringParts
    {
        private readonly string _a;
        private readonly string _b;
        private readonly string _c;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ThreeStringParts(string a, string b, string c)
        {
            _a = a;
            _b = b;
            _c = c;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FourStringParts With(string part) =>
            new FourStringParts(_a, _b, _c, part);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator string(ThreeStringParts self) =>
            self._a + self._b + self._c;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() =>
            this;
    }
}