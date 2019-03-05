using System;

namespace Pocket.Common
{
  public class Toggle<T>  where T : IEquatable<T>
  {
    private readonly T _default;
    private readonly T _or;
    
    private T _current;

    public Toggle(T @default, T or)
    {
      _default = @default;
      _or = or;
      
      _current = _default;
    }

    public T Use()
    {
      var current = _current;

      _current = current.Equals(_default) ? _or : _default;

      return current;
    }

    public void Reset() => _current = _default;

    public T UseAndReset()
    {
      var value = Use();
      
      Reset();

      return value;
    }
  }
}