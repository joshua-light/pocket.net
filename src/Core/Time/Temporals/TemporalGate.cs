using System;

namespace Pocket.Time
{
    public class TemporalGate : ITemporal
    {
        private readonly int _ms;
        
        private double _current;

        public TemporalGate(int ms) =>
            _ms = ms;
        
        public void Exist(TimeSpan span) =>
            _current += span.TotalMilliseconds;

        public bool Open()
        {
            if (_current < _ms)
                return false;

            _current = 0;
            return true;
        }
    }
}