using System.Threading;

namespace Pocket.Common
{
  public static class Concurrent
  {
    public static int Change(ref int self, int to, int when) =>
      Interlocked.CompareExchange(ref self, to, when);
  }
}