using Pocket.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Tests.ObjectTree.Nodes
{
    public class PrimitiveNodeTest
    {
        [Fact] public void Of_ShouldReturnNull_IfValueIsOfObjectType() =>
            Node<object>(of: new object()).ShouldBeNull();

        [Fact] public void Of_ShouldReturnNotNull_IfValueIsInt() =>
            Node<int>(of: 1).ShouldNotBeNull();
        [Fact] public void Of_ShouldReturnNotNull_IfValueIsString() =>
            Node<string>(of: "").ShouldNotBeNull();
        
        [Fact] public void Of_ShouldReturnNodeWithSpecifiedValue() =>
            Node<string>(of: "").Value.ShouldBe("");
        
        private static Node Node<T>(object of) =>
            PrimitiveNode.Of(typeof(T), of);
    }
}