using System.Collections.Generic;

namespace Pocket
{
    public static class SetExtensions
    {
        public static void Exclude<T>(this ISet<T> self, IEnumerable<T> items) =>
            self.ExceptWith(items);
    }
}