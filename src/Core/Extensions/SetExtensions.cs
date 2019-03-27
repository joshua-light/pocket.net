using System.Collections.Generic;

namespace Pocket.Common
{
    public static class SetExtensions
    {
        public static void Exclude<T>(this ISet<T> self, IEnumerable<T> items) =>
            self.ExceptWith(items);
    }
}