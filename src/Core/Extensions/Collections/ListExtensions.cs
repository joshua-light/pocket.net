using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket
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
    }
}