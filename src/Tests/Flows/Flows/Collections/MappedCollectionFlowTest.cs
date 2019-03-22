using System;
using System.Linq;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Flows.Collections
{
    public class MappedCollectionFlowTest
    {
        [Fact]
        public void Add_ShouldInvokeMapFunction()
        {
            var map = Substitute.For<Func<int, string>>();
            var flux = new PureCollectionFlux<int>();
            var flow = flux.Select(map);

            flow.Added.OnNext(Substitute.For<Action<string>>());
            flux.Add(10);

            map.Received(1).Invoke(10);
        }
        
        [Fact]
        public void Remove_ShouldInvokeMapFunction()
        {
            var map = Substitute.For<Func<int, string>>();
            var flux = new PureCollectionFlux<int>(new [] { 10 });
            var flow = flux.Select(map);

            flow.Removed.OnNext(Substitute.For<Action<string>>());
            flux.Remove(10);

            map.Received(1).Invoke(10);
        }

        [Fact]
        public void Current_ShouldBeEqualToMappedCollection()
        {
            var flux = new PureCollectionFlux<int>(Enumerable.Range(0, 5));
            var mapped = flux.Select(x => x.ToString());
            
            Assert.Equal(new []{ "0", "1", "2", "3", "4" }, mapped.Current.ToArray());
        }
    }
}