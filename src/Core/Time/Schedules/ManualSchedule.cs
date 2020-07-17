using System;
using System.Collections.Generic;

namespace Pocket.Time
{
    public class ManualSchedule : ISchedule
    {
        private class ScheduledPromise : IPromise
        {
            private readonly ManualPromise _promise = new ManualPromise();

            public ScheduledPromise(int ms) =>
                Ms = ms;
            
            public int Ms { get; private set; }

            public void Then(Action @do) =>
                _promise.Then(@do);
            
            public IPromise Then(Func<IPromise> @do) =>
                _promise.Then(@do);

            public void Exist(int ms)
            {
                Ms -= ms;
                
                if (Ms <= 0)
                    _promise.Satisfy();
            }
        }
        
        private readonly List<ScheduledPromise> _promises = new List<ScheduledPromise>();

        public IPromise Wait(int ms) =>
            new ScheduledPromise(ms).Do(_promises.Add);

        public void Skip(int ms)
        {
            while (_promises.Count > 0 && ms > 0)
            {
                var maxElapsed = 0;

                for (int i = 0, count = _promises.Count; i < count; i++)
                {
                    maxElapsed = _promises[i].Ms;
                    
                    _promises[i].Exist(ms);
                }
            
                _promises.RemoveAll(x => x.Ms <= 0);

                ms = (ms - maxElapsed).ButNotLess(than: 0);
            }
        }
    }
}