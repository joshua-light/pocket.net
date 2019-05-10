namespace Pocket.Common
{
    public class TextRepresentation
    {
        public static TextRepresentation Of<T>(T x) => Of((object) x);
        public static TextRepresentation Of(object x) => null;
        
        private readonly object _x;

        public TextRepresentation(object x) =>
            _x = x;

        public override string ToString() => "";
    }
}