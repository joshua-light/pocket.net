using System;
using System.Linq;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Flows.Collections
{
    public class MergedCollectionFlowTest
    {
        [Fact]
        public void Current_ShouldBeEqualToSumOfEnumerables()
        {
            var a = new PureCollectionFlux<int>(Enumerable.Range(0, 5));
            var b = new PureCollectionFlux<int>(Enumerable.Range(5, 5));
            var flow = new[] { a, b }.Merge();

            Assert.Equal(a.Current.Concat(b.Current), flow.Current);
        }

        [Fact]
        public void Added_ShouldBeCalled_IfAnyOfCollectionsChange() =>
            ShouldBeCalled(x => x.Added, (x, y) => x.Add(y));
        
        [Fact]
        public void Added_ShouldNotBeCalled_IfDisposed() =>
            ShouldNotBeCalled(x => x.Added, (x, y) => x.Add(y));

        [Fact]
        public void Removed_ShouldBeCalled_IfAnyOfCollectionsChange() =>
            ShouldBeCalled(x => x.Removed, (x, y) => x.Remove(y));
        
        [Fact]
        public void Removed_ShouldNotBeCalled_IfDisposed() =>
            ShouldNotBeCalled(x => x.Removed, (x, y) => x.Remove(y));

        #region Helpers

        void ShouldBeCalled(Func<ICollectionFlow<int>, IFlow<int>> flow, Action<ICollectionFlux<int>, int> call)
        {
            var action = Substitute.For<Action<int>>();
            var a = new PureCollectionFlux<int>(Enumerable.Range(0, 5));
            var b = new PureCollectionFlux<int>(Enumerable.Range(5, 5));
            var collection = new[] { a, b }.Merge();
            flow(collection).OnNext(action);

            call(a, 0);
            action.Received(1).Invoke(0);

            call(b, 5);
            action.Received(1).Invoke(5);
        }

        void ShouldNotBeCalled(Func<ICollectionFlow<int>, IFlow<int>> flow, Action<ICollectionFlux<int>, int> call)
        {
            var action = Substitute.For<Action<int>>();
            var a = new PureCollectionFlux<int>(Enumerable.Range(0, 5));
            var b = new PureCollectionFlux<int>(Enumerable.Range(5, 5));
            var collection = new[] { a, b }.Merge();
            var onNext = flow(collection).OnNext(action);

            call(a, 0);
            action.Received(1).Invoke(0);
            action.ClearReceivedCalls();

            onNext.Dispose();
            call(b, 5);
            action.DidNotReceive().Invoke(Arg.Any<int>());
        }

        #endregion
    }
}