using System;
using static Pocket.Common.Guard;

namespace Pocket.Common.Flows
{
    internal class MappedFlow<TIn, TOut> : IFlow<TOut>
    {
        private readonly IFlow<TIn> _source;
        private readonly Func<TIn, TOut> _map;

        public MappedFlow(IFlow<TIn> source, Func<TIn, TOut> map)
        {
            Ensure(source).NotNull();
            Ensure(map).NotNull();
            
            _source = source;
            _map = map;
        }
        
        public TOut Current => _map(_source.Current);

        public IDisposable OnNext(Action<TOut> action)
        {
            Ensure(action).NotNull();
            
            return _source.OnNext(x => action(_map(x)));
        }
    }
}