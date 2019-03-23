using System;
using System.Collections.Generic;

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
        When(_this != null, @throw: () => new ArgumentException(because));
      
      public void Is(T value) => Is(value, $"Expected that [ {_this} ] is equal to [ {value} ].");
      public void Is(T value, string because) =>
        When(!Equals(_this, value), @throw: () => new ArgumentException(because));
      
      public void IsNot(T value) => IsNot(value, $"Expected that [ {_this} ] is not equal to [ {value} ].");
      public void IsNot(T value, string because) =>
        When(Equals(_this, value), @throw: () => new ArgumentException(because));

      private static bool Equals(T x, T y) => EqualityComparer<T>.Default.Equals(x, y);
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

      public void Is<T>() => Is(typeof(T));
      public void Is(Type type) =>
        When(!_this.Is(type), @throw: () => new ArgumentException($"Specified type must be [ {type.PrettyName()} ]."));

      public void Derives<T>() => Derives(typeof(T));
      public void Derives(Type type) =>
        When(!_this.Derives(type), @throw: () => new ArgumentException($"Specified type must derive from [ {type.PrettyName()} ]."));

      public void IsOrDerives<T>() => IsOrDerives(typeof(T));
      public void IsOrDerives(Type type) =>
        When(!_this.Is(type) && !_this.Derives(type), @throw: () => new ArgumentException($"Specified type must be (or derive from) [ {type.PrettyName()} ]."));
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
        throw @throw();
    }
  }
}