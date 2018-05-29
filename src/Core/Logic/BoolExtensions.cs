namespace Pocket.Common
{
    public static class BoolExtensions
    {
        public static bool And(this bool self, bool other) => self && other;
        public static bool Or(this bool self, bool other) => self || other;

        public static bool Implication(this bool self, bool other) => !self || other;
        public static bool Equivalence(this bool self, bool other) => !self && !other || self && other;
    }
}