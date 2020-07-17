using System;

namespace Pocket.Flows
{
    public interface IVoidFlow
    {
        IDisposable OnNext(Action action);
    }
}