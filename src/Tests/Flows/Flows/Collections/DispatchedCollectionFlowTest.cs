using System;
using System.Collections.Generic;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Flows.Collections
{
    public class DispatchedCollectionFlowTest
    {
        [Fact]
        public void Dispatcher_ShouldBeCalled_OnAdd()
        {
            var action = Substitute.For<Action<Action>>();
            var flow = new PureCollectionFlux<int>(new List<int> {1, 2, 3});
            flow
                .DispatchedWith(action)
                .Added.OnNext(Substitute.For<Action<int>>());

            flow.Add(4);
            flow.Add(5);

            action.Received(2).Invoke(Arg.Any<Action>());
        }

        [Fact]
        public void Dispatcher_ShouldBeCalled_OnRemove()
        {
            var action = Substitute.For<Action<Action>>();
            var flow = new PureCollectionFlux<int>(new List<int> {1, 2, 3});
            flow
                .DispatchedWith(action)
                .Removed.OnNext(Substitute.For<Action<int>>());

            flow.Remove(2);
            flow.Remove(3);

            action.Received(2).Invoke(Arg.Any<Action>());
        }
    }
}