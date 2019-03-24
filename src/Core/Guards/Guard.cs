using System;
using System.Collections.Generic;
using System.Linq;

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
        NotNull(because: "Specified value should be not null.");
      public void NotNull(string because) =>
        When(_this == null, @throw: () => new ArgumentNullException("", because));
      
      public void Null() =>
        Null(because: $"[ {_this} ] should be null.");
      public void Null(string because) =>
        When(_this != null, @throw: because);
      
      public void Is(T value) =>
        Is(value, $"[ {_this} ] should be [ {value} ].");
      public void Is(T value, string because) =>
        When(!Equals(_this, value), @throw: because);
      
      public void IsNot(T value) =>
        IsNot(value, $"[ {_this} ] should not be [ {value} ].");
      public void IsNot(T value, string because) =>
        When(Equals(_this, value), @throw: because);

      public void Less(T than) =>
        Less(than, $"[ {_this} ] should be less than [ {than} ].");
      public void Less(T than, string because) =>
        When(Compared(_this, than) >= 0, @throw: because);

      public void LessOrEqual(T to) =>
        LessOrEqual(to, $"[ {_this} ] should be less or equal to [ {to} ].");
      public void LessOrEqual(T to, string because) =>
        When(Compared(_this, to) > 0, @throw: because);

      public void Greater(T than) =>
        Greater(than, $"[ {_this} ] should be greater than [ {than} ].");
      public void Greater(T than, string because) =>
        When(Compared(_this, than) <= 0, @throw: because);

      public void GreaterOrEqual(T to) =>
        GreaterOrEqual(to, $"[ {_this} ] should be greater or equal to [ {to} ].");
      public void GreaterOrEqual(T to, string because) =>
        When(Compared(_this, to) < 0, @throw: because);

      public void InRange(T from, T to) =>
        InRange(from, to, $"[ {_this} ] should be in range [ {from}, {to} ].");
      public void InRange(T from, T to, string because)
      {
        Ensure(from).LessOrEqual(from);
        
        When(Compared(_this, from) < 0 || Compared(_this, to) > 0, @throw: because);
      }
      
      private static bool Equals(T x, T y) => EqualityComparer<T>.Default.Equals(x, y);

      private static int Compared(T x, T y) => x is IComparable<T> c
        ? c.CompareTo(y)
        : throw new ArgumentException($"[ {x.GetType().PrettyName()} ] should implement [ {typeof(IComparable<T>).PrettyName()} ].");
    }

    public struct BoolExpression
    {
      private readonly bool _this;

      public BoolExpression(bool @this) =>
        _this = @this;

      public void True() =>
        True($"[ {_this} ] should be true.");
      public void True(string because) =>
        When(!_this, @throw: because);
      
      public void False() =>
        False($"[ {_this} ] should be false.");
      public void False(string because) =>
        When(_this, @throw: because);
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
        Is(typeof(T));
      public void Is<T>(string because) =>
        Is(typeof(T), because);
      public void Is(Type type) =>
        Is(type, $"[ {_this.PrettyName()} ] should be [ {type.PrettyName()} ].");
      public void Is(Type type, string because) =>
        When(!_this.Is(type), @throw: because);

      public void Derives<T>() =>
        Derives(typeof(T));
      public void Derives<T>(string because) =>
        Derives(typeof(T), because);
      public void Derives(Type type) =>
        Derives(type, $"Specified type must derive from [ {type.PrettyName()} ].");
      public void Derives(Type type, string because) =>
        When(!_this.Derives(type), @throw: because);

      public void IsOrDerives<T>() =>
        IsOrDerives(typeof(T));
      public void IsOrDerives<T>(string because) =>
        IsOrDerives(typeof(T), because);
      public void IsOrDerives(Type type) =>
        IsOrDerives(type, $"Specified type must be (or derive from) [ {type.PrettyName()} ].");
      public void IsOrDerives(Type type, string because) =>
        When(!_this.Is(type) && !_this.Derives(type), @throw: because);
    }

    public struct EnumerableExpression<T>
    {
      public IEnumerable<T> _this;

      public EnumerableExpression(IEnumerable<T> @this) =>
        _this = @this;
      
      public void NotNull() =>
        Common(_this).NotNull();
      public void NotNull(string because) =>
        Common(_this).NotNull(because);
      
      public void Null() =>
        Common(_this).Null();
      public void Null(string because) =>
        Common(_this).Null(because);

      public void Empty() =>
        When(_this, x => !x.IsNullOrEmpty(), @throw: x => $"{x.ToList().AsString()} should be empty.");
      public void Empty(string because) =>
        When(!_this.IsNullOrEmpty(), @throw: because);

      public void NotEmpty() =>
        NotEmpty($"Specified value should be not empty.");
      public void NotEmpty(string because) =>
        When(_this.IsNullOrEmpty(), @throw: because);
    }

    public static Expression<T> Ensure<T>(T that) =>
      new Expression<T>(that);
    public static BoolExpression Ensure(bool that) =>
      new BoolExpression(that); 
    public static TypeExpression Ensure(Type that) =>
      new TypeExpression(that);
    public static EnumerableExpression<T> Ensure<T>(IEnumerable<T> that) =>
      new EnumerableExpression<T>(that);

    private static Expression<T> Common<T>(T value) =>
      new Expression<T>(value);
    
    
    private static void When(bool fact, string @throw) =>
      When(fact, @throw: () => new ArgumentException(@throw));

    private static void When(bool fact, Func<Exception> @throw) =>
      When<bool>(default, _ => fact, _ => @throw());

    private static void When<T>(T value, Func<T, bool> fact, Func<T, string> @throw) =>
      When(value, fact, @throw: x => new ArgumentException(@throw(x)));
    
    private static void When<T>(T value, Func<T, bool> fact, Func<T, Exception> @throw)
    {
      if (fact(value))
        throw @throw(value);
    }
  }
}