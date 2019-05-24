using System.Collections.Generic;
using System.Linq;
using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.ObjectTree.Nodes
{
    public class CollectionNodeTest
    {
        [Fact] public void Of_ShouldReturnNull_IfValueIsNotEnumerable() =>
            Node<object>(of: new object()).ShouldBeNull();

        [Fact] public void Of_ShouldReturnCollectionNode_IfValueIsEnumerable() =>
            Node<IEnumerable<int>>(of: Enumerable.Range(0, 10)).ShouldBeOfType<CollectionNode>();

        [Fact] public void Value_ShouldEqualToSpecifiedCollection() =>
            Node<int[]>(of: new[] { 1, 2, 3 }).Value.ShouldBe(new[] { 1, 2, 3 });
        [Fact] public void Children_ShouldEqualToConcreteElementsOfCollection() =>
            Node<int[]>(of: new[] { 1, 2, 3 }).Children.Select(x => (int) x.Value).ShouldBe(new [] { 1, 2, 3 });
        
        private static Node Node<T>(object of) =>
            CollectionNode.Of(typeof(T), of);
    }
}