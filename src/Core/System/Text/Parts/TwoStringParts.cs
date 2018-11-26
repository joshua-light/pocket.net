using System.Runtime.CompilerServices;

namespace Pocket.Common
{
    public struct TwoStringParts
    {
        private readonly string _a;
        private readonly string _b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TwoStringParts(string a, string b)
        {
            _a = a;
            _b = b;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ThreeStringParts With(string part) =>
            new ThreeStringParts(_a, _b, part);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator string(TwoStringParts self) =>
            self._a + self._b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() =>
            this;
    }
}