using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.ObjectTree.Nodes
{
    public class NullNodeTest
    {
        [Fact] public void Of_ShouldReturnNull_IfValueIsNotNull() =>
            Node<string>(of: "").ShouldBeNull();

        [Fact] public void Of_ShouldReturnNullNode_IfValueIsNull() =>
            Node<string>(of: null).ShouldBeOfType<NullNode>();
        
        private static Node Node<T>(object of) =>
            NullNode.Of(typeof(T), of);
    }
}