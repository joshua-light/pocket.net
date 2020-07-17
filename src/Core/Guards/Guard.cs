using System;
using System.Collections.Generic;

namespace Pocket
{
  public static class Guard
  {
    public static GuardExpression<T> Ensure<T>(T that) =>
      new GuardExpression<T>(that);
    public static GuardBoolExpression Ensure(bool that) =>
      new GuardBoolExpression(that); 
    public static GuardTypeExpression Ensure(Type that) =>
      new GuardTypeExpression(that);
    public static GuardEnumerableExpression<T> Ensure<T>(IEnumerable<T> that) =>
      new GuardEnumerableExpression<T>(that);

    internal static void When(bool fact, string @throw) =>
      When(fact, @throw: () => new ArgumentException(@throw));
    internal static void When(bool fact, Func<Exception> @throw) =>
      When<bool>(default, _ => fact, _ => @throw());
    internal static void When<T>(T value, Func<T, bool> fact, Func<T, string> @throw) =>
      When(value, fact, @throw: x => new ArgumentException(@throw(x)));
    
    private static void When<T>(T value, Func<T, bool> fact, Func<T, Exception> @throw)
    {
      if (fact(value))
        throw @throw(value);
    }
  }
}