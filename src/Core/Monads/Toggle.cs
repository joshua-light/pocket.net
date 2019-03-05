using System;

namespace Pocket.Common
{
  public static class Toggle
  {
    public static Toggle<bool> Bool() => new Toggle<bool>(off: false, on: true);
  }
  
  public class Toggle<T>  where T : IEquatable<T>
  {
    private readonly T _off;
    private readonly T _on;
    
    private T _current;

    public Toggle(T off, T on)
    {
      _off = off;
      _on = on;
      
      _current = _off;
    }

    public T Use()
    {
      var current = _current;

      _current = current.Equals(_off) ? _on : _off;

      return current;
    }

    public void Reset() => _current = _off;

    public T UseAndReset()
    {
      var value = Use();
      
      Reset();

      return value;
    }
  }
}