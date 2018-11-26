using System.Runtime.CompilerServices;

namespace Pocket.Common
{
    public static class StringPartsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TwoStringParts With(this string self, string other) =>
            new TwoStringParts(self, other);
    }
}