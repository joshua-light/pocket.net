using System.Threading;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Threading
{
    public class ManualResetEventGateTest
    {
        [Fact]
        public void IsOpened_ShouldBeTrue_IfConstructedAsOpened() =>
            new ManualResetEventGate(opened: true).IsOpened.ShouldBeTrue();
        
        [Fact]
        public void IsOpened_ShouldBeFalse_IfConstructedAsClosed() =>
            new ManualResetEventGate(opened: false).IsOpened.ShouldBeFalse();

        [Fact]
        public void IsOpened_ShouldBeTrue_IfGateWasOpened()
        {
            var gate = new ManualResetEventGate(opened: false);
            
            gate.Open();
            
            gate.IsOpened.ShouldBeTrue();
        }

        [Fact]
        public void IsOpened_ShouldBeFalse_IfGateWasClosed()
        {
            var gate = new ManualResetEventGate(opened: true);
            
            gate.Close();
            
            gate.IsOpened.ShouldBeFalse();
        }

        [Fact]
        public void WaitForOpen_ShouldFreezeThread_UntilGateIsOpened()
        {
            var waits = false;
            
            var outerGate = new ManualResetEventGate(opened: false);
            var innerGate = new ManualResetEventGate(opened: false);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                innerGate.WaitForOpen();
                Thread.Sleep(20);
                
                if (waits)
                    outerGate.Open();
            });
            
            innerGate.Open();
            waits = true;
            outerGate.WaitForOpen();
        }
        
        [Fact]
        public void WaitForOpen_ShouldReturnFalse_IfGateWasNotOpenedWithinTimeout()
        {
            var gate = new ManualResetEventGate(opened: false);
            
            gate.WaitForOpen(5).ShouldBeFalse();
        }
        
        [Fact]
        public void WaitForOpen_ShouldReturnTrue_IfGateIsOpened()
        {
            var gate = new ManualResetEventGate(opened: true);
            
            gate.WaitForOpen(5).ShouldBeTrue();
        }
    }
}