using System;

namespace Pocket.Common.Time
{
    /// <summary>
    ///     Represents <see cref="IPromise"/> that will be applied after manual <see cref="Satisfy"/> call.
    /// </summary>
    public sealed class ManualPromise : IPromise
    {
        private Action _do;
        private bool _isSatisfied;
        
        public void Then(Action @do)
        {
            _do = @do;
            
            if (_isSatisfied)
                _do();
        }

        public IPromise Then(Func<IPromise> @do)
        {
            var promise = new ManualPromise();
            
            Then(() => @do()
                .Then(() => promise
                    .Satisfy())
            );

            return promise;
        }

        /// <summary>
        ///     Manually satisfies <see cref="IPromise"/>.
        /// </summary>
        /// <returns><code>this</code> object.</returns>
        public void Satisfy()
        {
            if (_isSatisfied)
                return;
            
            _isSatisfied = true;
            _do?.Invoke();
        }
    }
}