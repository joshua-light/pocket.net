using System;
using NSubstitute;
using Pocket.Time;
using Xunit;

namespace Pocket.Tests.Time.Schedules
{
    public class ManualScheduleTest
    {
        [Fact]
        public void Skip_ShouldNotSatisfy_IfElapsedNotEnoughTime()
        {
            var action = Substitute.For<Action>();
            var schedule = new ManualSchedule();

            schedule.Wait(200).Then(action);
            schedule.Skip(100);
            schedule.Skip(99);

            action.Received(0).Invoke();
            
        }
        [Fact]
        public void Skip_ShouldSatisfy()
        {
            var action = Substitute.For<Action>();
            var schedule = new ManualSchedule();

            schedule.Wait(200).Then(action);
            schedule.Skip(100);
            schedule.Skip(105);

            action.Received(1).Invoke();
        }
        
        [Fact]
        public void Skip_ShouldSatisfyOneTime()
        {
            var action = Substitute.For<Action>();
            var schedule = new ManualSchedule();
            
            schedule.Wait(200).Then(action);
            schedule.Skip(205);
            schedule.Skip(205);
            
            action.Received(1).Invoke();
        }

        [Fact]
        public void Skip_ShouldSatisfy_IfPromiseRescheduleSelf()
        {
            var schedule = new ManualSchedule();
            var action = Substitute.For<Action>();

            schedule
                .Wait(100)
                .Then(() => schedule.Wait(100).Then(action));

            schedule.Skip(200);
            
            action.Received(1).Invoke();
        }
    }
}