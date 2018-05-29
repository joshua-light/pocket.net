namespace Pocket.Common
{
    public interface IPool<T> where T : class
    {
        T Take();
        
        void Release(T item);
    }
}