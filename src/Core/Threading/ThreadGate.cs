using System;
using System.Threading;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents gate that can be used for threads synchronization in same way as <see cref="ManualResetEvent"/>.
    /// </summary>
    public class ThreadGate
    {
        public static ThreadGate Opened => new ThreadGate(opened: true);
        public static ThreadGate Closed => new ThreadGate(opened: false);
        
        private readonly ManualResetEvent _event;
        
        /// <summary>
        ///     Initializes new instance of <see cref="ThreadGate"/>.
        /// </summary>
        /// <param name="opened">Initial gate state.</param>
        public ThreadGate(bool opened = true)
        {
            _event = new ManualResetEvent(opened);
        }

        /// <summary>
        ///     Determines whether gate is opened.
        /// </summary>
        public bool IsOpened => _event.WaitOne(0);

        /// <summary>
        ///     Opens gate after specified delay.
        /// </summary>
        /// <param name="after">Delay before gate is opened.</param>
        public void Open(TimeSpan after)
        {
            Thread.Sleep(after);
            
            Open();
        }
        
        /// <summary>
        ///     Opens gate.
        /// </summary>
        public void Open() => _event.Set();
        
        /// <summary>
        ///     Closes gate.
        /// </summary>
        public void Close() => _event.Reset();
        
        /// <summary>
        ///     Blocks current thread for specified amount of time (or infinite) until gate is became opened.
        /// </summary>
        /// <param name="timeoutMs">How much time thread should be blocked in milliseconds.</param>
        /// <returns><code>true</code> if gate was opened while thread was blocked, otherwise — <code>false</code>.</returns>
        public bool WaitForOpen(int timeoutMs = -1) => _event.WaitOne(timeoutMs);
    }
}
