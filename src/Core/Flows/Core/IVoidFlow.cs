using System;

namespace Pocket.Common.Flows
{
    public interface IVoidFlow
    {
        IDisposable OnNext(Action action);
    }
}