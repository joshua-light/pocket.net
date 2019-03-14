using System;
using System.Collections.Generic;
using System.Reflection;

namespace Pocket.Common
{
  public static class Instances
  {
    public struct AssemblyExpression<T>
    {
      private readonly Predicate<Type> _predicate;

      public AssemblyExpression(Predicate<Type> predicate) =>
        _predicate = predicate;

      public List<T> InThisAssembly() => In(Assembly.GetCallingAssembly());
      public List<T> In(Assembly assembly)
      {
        var all = new List<T>();
        
        foreach (var type in assembly.GetTypes())
          if (!type.IsAbstract && _predicate(type))
            all.Add(type.New<T>());

        return all;
      }
    }
    
    public static AssemblyExpression<T> Of<T>() where T : class =>
      new AssemblyExpression<T>(x => Implements(x, typeof(T)) || Extends(x, typeof(T)));
    
    public static AssemblyExpression<T> ThatExtend<T>() where T : class =>
      new AssemblyExpression<T>(x => Implements(x, typeof(T)));
    
    public static AssemblyExpression<T> ThatImplement<T>() where T : class =>
      new AssemblyExpression<T>(x => Extends(x, typeof(T)));

    private static bool Implements(Type self, Type other)
    {
      if (!other.IsInterface)
        return false;

      return other.IsConstructedGenericType
        ? self.Implements(other.GetGenericTypeDefinition())
        : self.Implements(other);
    }
    
    private static bool Extends(Type self, Type other)
    {
      if (!other.IsClass)
        return false;

      return other.IsConstructedGenericType
        ? self.Extends(other.GetGenericTypeDefinition())
        : self.Extends(other);
    }
  }
}