using System;
using System.Linq;
using NSubstitute;
using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Flows.Single
{
    public class SumFlowTest
    {
        #region IEnumerable

        [Fact]
        public void Current_ShouldBeZero_IfEnumerableIsEmpty()
        {
            var flow = Enumerable.Empty<IFlow<int>>().FlowSum(x => x);
            Assert.Equal(0, flow.Current);
        }

        [Fact]
        public void Current_ShouldEqualToItemsSum_IfEnumerableIsNotEmpty()
        {
            var items = new []
            {
                new PureFlux<string>("1"),
                new PureFlux<string>("2"),
                new PureFlux<string>("3")
            };
            var flow = items.FlowSum(x => x.Length);

            Assert.Equal(3, flow.Current);
        }

        [Fact]
        public void Current_ShouldEqualToItemsSum_IfItemsChange()
        {
            var items = new []
            {
                new PureFlux<string>("1"),
                new PureFlux<string>("2"),
                new PureFlux<string>("3")
            };
            var flow = items.FlowSum(x => x.Length);
            items[0].Pulse("123");
            items[1].Pulse("123");
            items[2].Pulse("123");
            
            Assert.Equal(9, flow.Current);
        }
        
        #endregion
        
        #region ICollectionFlow
        
        [Fact]
        public void Current_ShouldBeZero_IfCollectionIsEmpty()
        {
            var collection = new PureCollectionFlux<int>();
            var flow = new SumFlow<int>(collection, Substitute.For<Func<int, int, int>>(), Substitute.For<Func<int, int, int>>());

            Assert.Equal(0, flow.Current);
        }
        
        [Fact]
        public void Current_ShouldEqualToItemsSum_IfCollectionIsNotEmpty()
        {
            var collection = new PureCollectionFlux<int>();
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            var flow = collection.Sum(x => x);

            Assert.Equal(6, flow.Current);
        }

        [Fact]
        public void OnNext_ShoulBeCalledWithCorrectValue_IfItemWasAdded()
        {
            var onNext = Substitute.For<Action<int>>();
            var collection = new PureCollectionFlux<int>();
            var flow = collection.Sum(x => x);

            flow.OnNext(onNext);
            
            collection.Add(5);
            onNext.Received(1).Invoke(5);
            
            collection.Add(5);
            onNext.Received(1).Invoke(10);
            
            collection.Remove(5);
            onNext.Received(2).Invoke(5);
        }
        
        #endregion
    }
}