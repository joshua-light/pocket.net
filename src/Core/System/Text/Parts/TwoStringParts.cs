namespace Pocket.Common
{
    public struct TwoStringParts
    {
        private readonly string _a;
        private readonly string _b;

        public TwoStringParts(string a, string b)
        {
            _a = a;
            _b = b;
        }
        
        public ThreeStringParts With(string part) =>
            new ThreeStringParts(_a, _b, part);

        public static implicit operator string(TwoStringParts self) =>
            self._a + self._b;
    }
}