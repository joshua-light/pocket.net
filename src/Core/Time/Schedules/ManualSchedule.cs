using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Time
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
            for (var i = 0; i < _promises.Count; i++)
                _promises[i].Exist(ms);
            
            _promises.RemoveAll(x => x.Ms <= 0);
        }
    }
}