namespace Pocket.Common
{
  public static class CodeExtensions
  {
    public static Code Text(this Code self, string text, bool when) =>
        when ? self.Text(text) : self;
  }
}