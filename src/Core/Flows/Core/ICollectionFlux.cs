namespace Pocket.Common.Flows
{
    public interface ICollectionFlux<T> : ICollectionFlow<T>
    {
        Result Add(T item);
        Result Remove(T item);
    }
}