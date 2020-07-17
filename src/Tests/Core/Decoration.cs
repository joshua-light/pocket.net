using System;
using NSubstitute;

namespace Pocket.Tests.Core
{
    public class Decoration<T> where T : class 
    {     
        private readonly T _sut;

        public Decoration(T sut)
        {
            _sut = sut;
        }

        public Decoration<T> Call<TValue>(Func<T, TValue> call, Action<CallAssertion> assert)
        {
            call(_sut);
            assert(new CallAssertion(x => call(x)));

            return this;
        }

        public Decoration<T> Call(Action<T> call, Action<CallAssertion> assert)
        {
            call(_sut);
            assert(new CallAssertion(call));

            return this;
        }

        #region Inner Classes

        public class CallAssertion
        {
            private readonly Action<T> _call;

            public CallAssertion(Action<T> call)
            {
                _call = call;
            }

            public CallAssertion Decorates(T item)
            {
                _call(item.Received(1));
                return this;
            }

            public CallAssertion Decorates<TItem>(TItem item, Action<TItem> call) where TItem : class
            {
                call(item.Received());
                return this;
            }
        }

        #endregion
    }
}