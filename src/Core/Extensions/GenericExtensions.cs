namespace Pocket.Common
{
    public static class GenericExtensions
    {
        public static T Or<T>(this T self, T @default) where T : class =>
            self ?? @default;
    }
}