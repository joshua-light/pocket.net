using System.Threading;
using Pocket.System;
using Shouldly;
using Xunit;

namespace Pocket.Tests.Threading
{
    public class GateTest
    {
        [Fact]
        public void IsOpened_ShouldBeTrue_IfConstructedAsOpened() =>
            new Gate(opened: true).IsOpened.ShouldBeTrue();
        
        [Fact]
        public void IsOpened_ShouldBeFalse_IfConstructedAsClosed() =>
            new Gate(opened: false).IsOpened.ShouldBeFalse();

        [Fact]
        public void IsOpened_ShouldBeTrue_IfGateWasOpened()
        {
            var gate = new Gate(opened: false);
            
            gate.Open();
            
            gate.IsOpened.ShouldBeTrue();
        }

        [Fact]
        public void IsOpened_ShouldBeFalse_IfGateWasClosed()
        {
            var gate = new Gate(opened: true);
            
            gate.Close();
            
            gate.IsOpened.ShouldBeFalse();
        }

        [Fact]
        public void WaitForOpen_ShouldFreezeThread_UntilGateIsOpened()
        {
            var waits = false;
            
            var outerGate = new Gate(opened: false);
            var innerGate = new Gate(opened: false);

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
            var gate = new Gate(opened: false);
            
            gate.WaitForOpen(5).ShouldBeFalse();
        }
        
        [Fact]
        public void WaitForOpen_ShouldReturnTrue_IfGateIsOpened()
        {
            var gate = new Gate(opened: true);
            
            gate.WaitForOpen(5).ShouldBeTrue();
        }
    }
}