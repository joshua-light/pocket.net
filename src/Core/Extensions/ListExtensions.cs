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

        private struct ListRange<T>
        {
            public static readonly ListRange<T> Empty = new ListRange<T>(null, 0, 0);
            
            private readonly int _from;

            public ListRange(IList<T> list, int from, int count)
            {
                _from = from;

                List = list;
                Count = count;
            }
            
            public IList<T> List { get; }
            public int Count { get; }
            public bool IsEmpty => Count == 0;

            public ListRange<T> From(int from)
            {
                
            }
        }

        public static IList<IList<T>> Permutations<T>(this IList<T> self)
        {
            if (self.IsEmpty())
                return new List<IList<T>>();
            
            return Permutations(Range(from: 0), Empty());

            ListRange<T> Empty() => ListRange<T>.Empty;
            ListRange<T> Range(int from) => new ListRange<T>(self, from, self.Count);
        }
    }
}