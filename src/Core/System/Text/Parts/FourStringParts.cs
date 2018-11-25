namespace Pocket.Common
{
    public struct FourStringParts
    {
        private readonly string _a;
        private readonly string _b;
        private readonly string _c;
        private readonly string _d;

        public FourStringParts(string a, string b, string c, string d)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }
        
        public StringParts With(string part) =>
            new StringParts(_a, _b, _c, _d, part);

        public static implicit operator string(FourStringParts self) =>
            self._a + self._b + self._c + self._d;

        public override string ToString() =>
            this;
    }
}