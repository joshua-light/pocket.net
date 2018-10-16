using System;

namespace Pocket.Common
{
  public class Toggle<T>  where T : IEquatable<T>
  {
    private readonly T _a;
    private readonly T _b;
    
    private T _current;

    public Toggle(T a, T b)
    {
      _a = a;
      _b = b;
      
      _current = _a;
    }

    public T Use()
    {
      var current = _current;

      _current = current.Equals(_a) ? _b : _a;

      return current;
    }
  }
}