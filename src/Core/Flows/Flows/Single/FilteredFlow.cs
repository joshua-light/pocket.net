using System;

namespace Pocket.Flows
{
    internal class FilteredFlow<T> : IFlow<T>
    {
        private readonly IFlow<T> _source;
        private readonly Func<T, bool> _predicate;

        public FilteredFlow(IFlow<T> source, Func<T, bool> predicate)
        {
            _source = source;
            _predicate = predicate;
        }

        public T Current => _source.Current;

        public IDisposable OnNext(Action<T> action) => _source.OnNext(x =>
        {
            if (!_predicate(x))
                return;

            action(x);
        });
    }
}