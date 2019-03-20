using System;

namespace Pocket.Common
{
  public static class Guard
  {
    public struct Expression<T>
    {
      private readonly T _this;

      public Expression(T @this) =>
        _this = @this;

      public void NotNull() =>
        NotNull(because: "Specified value must be not null.");
      public void NotNull(string because) =>
        When(_this == null, @throw: () => new ArgumentNullException("", because));
      
      public void Null() =>
        Null(because: "Specified value must be null.");
      public void Null(string because) =>
        When(_this != null, @throw: () => new ArgumentNullException("", because));
    }

    public struct BoolExpression
    {
      private readonly bool _this;

      public BoolExpression(bool @this) =>
        _this = @this;

      public void True() => True("Specified value must be true.");
      public void True(string because) =>
        When(!_this, @throw: () => new ArgumentException(because));
      
      public void False() => False("Specified value must be false.");
      public void False(string because) =>
        When(_this, @throw: () => new ArgumentException(because));
    }

    public struct TypeExpression
    {
      private readonly Type _this;

      public TypeExpression(Type @this) =>
        _this = @this;

      public void NotNull() =>
        Common(_this).NotNull();
      public void NotNull(string because) =>
        Common(_this).NotNull(because);
      
      public void Null() =>
        Common(_this).Null();
      public void Null(string because) =>
        Common(_this).Null(because);
      
      public void Is<T>() =>
        When(!_this.Is<T>(), @throw: () => new ArgumentException($"Specified type must be [ {typeof(T).PrettyName()} ]."));
      public void Derives<T>() =>
        When(!_this.Derives<T>(), @throw: () => new ArgumentException($"Specified type must derive from [ {typeof(T).PrettyName()} ]."));
      public void IsOrDerives<T>() =>
        When(!_this.Is<T>() && !_this.Derives<T>(), @throw: () => new ArgumentException($"Specified type must be (or derive from) [ {typeof(T).PrettyName()} ]."));
    }

    public static Expression<T> Ensure<T>(T that) =>
      new Expression<T>(that);
    public static BoolExpression Ensure(bool that) =>
      new BoolExpression(that);
    public static TypeExpression Ensure(Type that) =>
      new TypeExpression(that);

    private static Expression<T> Common<T>(T value) =>
      new Expression<T>(value);
    
    private static void When(bool fact, Func<Exception> @throw)
    {
      if (fact)
        return;
        
      throw @throw();
    }
  }
}