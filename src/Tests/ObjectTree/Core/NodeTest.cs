using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.ObjectTree.Core
{
    public class NodeTest
    {
        [Fact] public void Of_ShouldReturnPrimitiveNode_IfValueIsString() =>
            Node<string>(of: "").ShouldBeOfType<PrimitiveNode>();

        private static Node Node<T>(object of) =>
            Common.ObjectTree.Node.Of(typeof(T), of);
    }
}