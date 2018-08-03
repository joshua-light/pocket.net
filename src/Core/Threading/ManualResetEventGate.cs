using System.Threading;

namespace Pocket.Common
{
    public struct ManualResetEventGate
    {
        private readonly ManualResetEvent _event;
        
        public ManualResetEventGate(bool opened = true)
        {
            _event = new ManualResetEvent(opened);
        }

        public bool IsOpened => _event.WaitOne(0);
        
        public void Open() => _event.Set();
        public void Close() => _event.Reset();
        public void WaitForOpen() => _event.WaitOne();
    }
}