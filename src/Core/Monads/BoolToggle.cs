namespace Pocket.Common
{
  public class BoolToggle : Toggle<bool>
  {
    public BoolToggle(bool @default, bool or) : base(@default, or) { }

    public static implicit operator bool(BoolToggle self) =>
      self.Current;
  }
}