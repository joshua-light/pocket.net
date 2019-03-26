using System.Threading;

namespace Pocket.Common
{
  public static class Atomically
  {
    public static bool Changed(ref int self, int from, int to) =>
      Change(ref self, from, to) == from;
    public static bool Changed(ref long self, long from, long to) =>
      Change(ref self, from, to) == from;
    public static bool Changed(ref float self, float from, float to) =>
      Change(ref self, from, to) == from;
    public static bool Changed(ref double self, double from, double to) =>
      Change(ref self, from, to) == from;
    public static bool Changed(ref object self, object from, object to) =>
      Change(ref self, from, to) == from;
    public static bool Changed<T>(ref T self, T from, T to) where T : class =>
      Change(ref self, from, to) == from;
    
    public static int Change(ref int self, int from, int to) =>
      Interlocked.CompareExchange(ref self, to, from);
    public static long Change(ref long self, long from, long to) =>
      Interlocked.CompareExchange(ref self, to, from);
    public static float Change(ref float self, float from, float to) =>
      Interlocked.CompareExchange(ref self, to, from);
    public static double Change(ref double self, double from, double to) =>
      Interlocked.CompareExchange(ref self, to, from);
    public static object Change(ref object self, object from, object to) =>
      Interlocked.CompareExchange(ref self, to, from);
    public static T Change<T>(ref T self, T from, T to) where T : class =>
      Interlocked.CompareExchange(ref self, to, from);

    public static int Increment(ref int self) =>
      Interlocked.Increment(ref self);
    public static long Increment(ref long self) =>
      Interlocked.Increment(ref self);
  }
}