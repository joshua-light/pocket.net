namespace Pocket.Common
{
    public static class StringPartsExtensions
    {
        public static TwoStringParts With(this string self, string other) =>
            new TwoStringParts(self, other);
    }
}