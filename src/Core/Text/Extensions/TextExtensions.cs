namespace Pocket
{
  public static class TextExtensions
  {
    public static IText With(this IText self, string text, bool when) =>
      when ? self.With(text) : self;
  }
}