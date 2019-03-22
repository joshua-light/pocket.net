using System.Collections.Generic;

namespace Pocket.Common.Flows
{
    public interface ICollectionFlow<out T>
    {
        IEnumerable<T> Current { get; }
        
        IFlow<T> Added { get; }
        IFlow<T> Removed { get; }
    }
}