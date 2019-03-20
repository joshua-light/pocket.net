using System;

namespace Pocket.Common
{
  public static class Guard
  {
    public struct GuardExpression<T>
    {
      private readonly T _this;

      public GuardExpression(T @this) =>
        _this = @this;

      public void NotNull() =>
        NotNull(because: "Specified value must be not null.");
      public void NotNull(string because) =>
        When(_this == null, @throw: () => new ArgumentNullException("", because));
      
      public void Null() =>
        NotNull(because: "Specified value must be null.");
      public void Null(string because) =>
        When(_this != null, @throw: () => new ArgumentNullException("", because));
    }

    public struct BoolGuardExpression
    {
      private readonly bool _this;

      public BoolGuardExpression(bool @this) =>
        _this = @this;

      public void True() => True("Specified value must be true.");
      public void True(string because) =>
        When(!_this, @throw: () => new ArgumentException(because));
      
      public void False() => False("Specified value must be false.");
      public void False(string because) =>
        When(_this, @throw: () => new ArgumentException(because));
    }

    public static GuardExpression<T> Ensure<T>(T that) =>
      new GuardExpression<T>(that);
    public static BoolGuardExpression Ensure(bool that) =>
      new BoolGuardExpression(that);

    private static GuardExpression<T> Common<T>(T value) =>
      new GuardExpression<T>(value);
    
    private static void When(bool fact, Func<Exception> @throw)
    {
      if (fact)
        return;
        
      throw @throw();
    }
  }
}