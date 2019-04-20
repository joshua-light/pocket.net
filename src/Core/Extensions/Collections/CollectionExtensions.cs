using System.Collections.Generic;

namespace Pocket.Common
{
  public static class CollectionExtensions
  {
    public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> other) =>
      other.ForEach(self.Add);
  }
}