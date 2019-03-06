namespace Pocket.Common
{
  public static class CodeExtensions
  {
    public static Code Text(this Code self, string text, bool when) =>
        when ? self.Text(text) : self;
    public static Code NewLine(this Code self, bool when) =>
        when ? self.NewLine() : self;

    public static CSharp CSharp(this Code self, int indent = 4) =>
        new CSharp(self, indent);
  }
}