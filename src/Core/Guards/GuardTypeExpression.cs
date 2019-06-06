using System;
using static Pocket.Common.Guard;

namespace Pocket.Common
{
    public struct GuardTypeExpression
    {
      private readonly Type _this;

      public GuardTypeExpression(Type @this) =>
        _this = @this;

      public void NotNull() =>
        Common(_this).NotNull();
      public void NotNull(string because) =>
        Common(_this).NotNull(because);
      
      public void Null() =>
        Common(_this).Null();
      public void Null(string because) =>
        Common(_this).Null(because);

      public void Has(Func<Type, bool> predicate) =>
        Common(_this).Has(predicate);
      public void Has(Func<Type, bool> predicate, string because) =>
        Common(_this).Has(predicate, because);
      
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
      
      private static GuardExpression<Type> Common(Type x) => new GuardExpression<Type>(x);
    }
}