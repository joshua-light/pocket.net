using System.Threading;

namespace Pocket.Common
{
  public static class Concurrent
  {
    public static int Change(ref int self, int from, int to) =>
      Interlocked.CompareExchange(ref self, to, from);
  }
}