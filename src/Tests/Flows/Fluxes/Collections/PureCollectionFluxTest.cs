using System;
using System.Linq;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Fluxes.Collections
{
    public class PureCollectionFluxTest
    {
        public class EmptyCollection
        {
            private readonly ICollectionFlux<int> _collection = new PureCollectionFlux<int>();

            [Fact]
            public void Current_ShouldReturnEmptyList() =>
                Assert.Empty(_collection.Current);

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(21)]
            public void AddedOnNext_ShouldBeCalledWhenItemIsAdded(int data)
            {
                var action = Substitute.For<Action<int>>();
                using (_collection.Added.OnNext(action))
                    _collection.Add(data);

                action.Received(1).Invoke(data);
            }
            
            [Fact]
            public void RemovedOnNext_ShouldNotBeCalledOnRemove()
            {
                var action = Substitute.For<Action<int>>();
                using (_collection.Added.OnNext(action))
                    _collection.Remove(10);

                action.DidNotReceive().Invoke(10);
            }
        }

        public class NotEmptyCollection
        {
            private readonly ICollectionFlux<int> _collection = new PureCollectionFlux<int>(Enumerable.Range(1, 5));

            [Fact]
            public void Current_ShouldBeNotEmpty() =>
                Assert.NotEmpty(_collection.Current);
            
            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(4)]
            public void RemovedOnNext_ShouldBeCalledWhenItemIsAdded(int data)
            {
                var action = Substitute.For<Action<int>>();
                using (_collection.Removed.OnNext(action))
                    _collection.Remove(data);

                action.Received(1).Invoke(data);
            }
        }

        public class VarianceTest
        {
            class Parent { }
            class Child : Parent { }
            
            private readonly ICollectionFlux<Child> _collection = new PureCollectionFlux<Child>();

            [Fact]
            public void SubscribeOnAdded_OnCastedCollection_ShouldSubscribeCorrectly()
            {
                var action = Substitute.For<Action<Parent>>();

                if (_collection is ICollectionFlow<Parent> flow)
                {
                    flow.Added.OnNext(action);
                    _collection.Add(new Child());
                }
                
                action.Received(1).Invoke(Arg.Any<Parent>());
            }
        }
    }
}