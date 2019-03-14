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
          if (_predicate(type))
            all.Add((T) type.New());

        return all;
      }
    }
    
    public static AssemblyExpression<T> Of<T>() where T : class =>
      new AssemblyExpression<T>(x => typeof(T).IsInterface && x.Implements<T>() || typeof(T).IsClass && x.Extends<T>());
    
    public static AssemblyExpression<T> ThatExtend<T>() where T : class =>
      new AssemblyExpression<T>(x => typeof(T).IsInterface && x.Implements<T>());
    
    public static AssemblyExpression<T> ThatImplement<T>() where T : class =>
      new AssemblyExpression<T>(x => typeof(T).IsClass && x.Extends<T>());
  }
}