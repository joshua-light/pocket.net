namespace Pocket.Common
{
  public interface IText
  {
    IText With(string text);
    IText NewLine();
  }
}