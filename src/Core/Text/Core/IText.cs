namespace Pocket
{
  public interface IText
  {
    bool IsEmpty { get; }
    
    IText With(string text);
    IText NewLine();
    
    string ToString();
  }
}