namespace Pocket.Common.ObjectTree
{
    public static class ObjectTreeExtensions
    {
        public static Node Tree<T>(this T self) =>
                      Node.Of(self);
        
    }
}