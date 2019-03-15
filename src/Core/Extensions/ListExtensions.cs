using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common
{
    public static class ListExtensions
    {
        public static string AsString<T>(this IList<T> self) =>
            self.AsString(x => x?.ToString() ?? "null");
        public static string AsString<T>(this IList<T> self, Func<T, object> asString) =>
            self.AsString(x => x != null ? asString(x) : "null");
        
        public static string AsString<T>(this IList<T> self, Func<T, string> asString)
        {
            if (self.IsNullOrEmpty())
                return "[]";

            return $"[ {self.Select(x => $"{asString(x) ?? "null"}").Separated(with: ", ")} ]";
        }

        public static IList<IList<T>> Permutations<T>(this IList<T> self)
        {
            if (self.IsEmpty())
                return new List<IList<T>>();

            return null;
        }
    }
}