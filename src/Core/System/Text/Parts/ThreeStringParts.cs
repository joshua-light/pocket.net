namespace Pocket.Common
{
    public struct ThreeStringParts
    {
        private readonly string _a;
        private readonly string _b;
        private readonly string _c;

        public ThreeStringParts(string a, string b, string c)
        {
            _a = a;
            _b = b;
            _c = c;
        }
        
        public FourStringParts With(string part) =>
            new FourStringParts(_a, _b, _c, part);

        public static implicit operator string(ThreeStringParts self) =>
            self._a + self._b + self._c;
        
        public override string ToString() =>
            this;
    }
}