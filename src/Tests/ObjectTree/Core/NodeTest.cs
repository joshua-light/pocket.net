using Pocket.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Tests.ObjectTree.Core
{
    public class NodeTest
    {
        private class Some { }
        
        [Fact] public void Of_ShouldReturnPrimitiveNode_IfValueIsString() =>
            Node<string>(of: "").ShouldBeOfType<PrimitiveNode>();
        [Fact] public void Of_ShouldReturnObjectNode_IfValueIsSomeType() =>
            Node<Some>(of: new Some()).ShouldBeOfType<ObjectNode>();

        private static Node Node<T>(object of) =>
            Pocket.ObjectTree.Node.Of(typeof(T), of);
    }
}