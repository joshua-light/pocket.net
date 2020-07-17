using Pocket.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Tests.ObjectTree.Nodes
{
    public class EmptyNodeTest
    {
        [Fact] public void Of_ShouldReturnNull_IfObjectIsNotOfTypeObject() =>
            Node<int>(of: 1).ShouldBeNull();
        
        [Fact] public void Of_ShouldReturnEmptyNode_IfObjectIsDefaultObject() =>
            Node<object>(of: new object()).ShouldBeOfType<EmptyNode>();
        
        private static Node Node<T>(object of) =>
            EmptyNode.Of(typeof(T), of);
    }
}