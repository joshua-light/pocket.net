namespace Pocket
{
    public static class MaybeExtensions
    {
        public static Maybe<T> Maybe<T>(this T self) where T : class => new Maybe<T>(self, self != null);
        public static Maybe<T> Just<T>(this T self) where T : struct => new Maybe<T>(self, true);
        public static T Or<T>(this Maybe<T> self, T defaultValue) => self.HasValue ? self.Value : defaultValue;
    }
}