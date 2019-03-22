using System;

namespace Pocket.Common.Flows
{
    internal class DispatchedFlow<T> : IFlow<T>
    {
        private readonly IFlow<T> _flow;
        private readonly Action<Action> _dispatcher;

        public DispatchedFlow(IFlow<T> flow, Action<Action> dispatcher)
        {
            _flow = flow;
            _dispatcher = dispatcher;
        }

        public T Current => _flow.Current;

        public IDisposable OnNext(Action<T> action) =>
            _flow.OnNext(x => Dispatch(() => action(x)));

        private void Dispatch(Action action) =>
            _dispatcher(action);
    }
}