using System;
using System.Linq;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Flows.Collections
{
    public class FilteredCollectionFlowTest
    {
        [Fact]
        public void AddedOrRemoved_ShouldNotBeCalled_IfPredicateIsFalse()
        {
            var action = Substitute.For<Action<int>>();
            var collection = new PureCollectionFlux<int>();
            var sut = collection.Where(x => x <= 10);
            sut.Added.OnNext(action);
            sut.Removed.OnNext(action);
            
            collection.Add(11);
            collection.Add(12);
            collection.Add(13);
            collection.Add(14);
            collection.Add(15);
            
            action.DidNotReceive().Invoke(Arg.Any<int>());
        }
        
        [Fact]
        public void Current_ShouldReturnFilteredCollection()
        {
            var collection = new PureCollectionFlux<int>();
            collection.Add(11);
            collection.Add(12);
            collection.Add(13);
            collection.Add(14);
            collection.Add(15);
            var sut = collection.Where(x => x <= 10);
            
            Assert.Equal(Enumerable.Empty<int>(), sut.Current);
        }
    }
}