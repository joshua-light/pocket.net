using System;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Flows.Single
{
    public class CountFlowTest
    {
        [Fact]
        public void Current_ShouldBeZero_IfCollectionIsEmpty()
        {
            var collection = new PureCollectionFlux<int>();
            var flow = collection.Count();

            Assert.Equal(0, flow.Current);
        }
        
        [Fact]
        public void Current_ShouldEqualToItemsCount_IfCollectionIsNotEmpty()
        {
            var collection = new PureCollectionFlux<int>();
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            var flow = collection.Count();

            Assert.Equal(3, flow.Current);
        }

        [Fact]
        public void OnNext_ShoulBeCalledWithCorrectValue_IfItemWasAdded()
        {
            var onNext = Substitute.For<Action<int>>();
            var collection = new PureCollectionFlux<int>();
            var count = collection.Count();

            count.OnNext(onNext);
            
            collection.Add(1);
            onNext.Received(1).Invoke(1);
            
            collection.Add(1);
            onNext.Received(1).Invoke(2);
            
            collection.Remove(1);
            onNext.Received(2).Invoke(1);
        }
    }
}