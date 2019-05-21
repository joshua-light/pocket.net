using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.ObjectTree
{
    public class ObjectTreeTests
    {
        public class DefaultObjectNode
        {
            private readonly object _object = new object();

            [Fact] public void TypeOfNode_ShouldBeEmptyNode() =>
                _object.Tree().ShouldBeOfType<EmptyNode>();
            [Fact] public void Type_ShouldBeObject() =>
                _object.Tree().Type.ShouldBe(typeof(object));
            [Fact] public void Value_ShouldBeSameAsDeclaredObject() =>
                _object.Tree().Value.ShouldBe(_object);
        }

        public class IntObjectNode
        {
            [Fact] public void TypeOfNode_ShouldBePrimitiveNode() =>
                1.Tree().ShouldBeOfType<PrimitiveNode>();
            [Fact] public void Type_ShouldBeInt() =>
                1.Tree().Type.ShouldBe(typeof(int));
            [Fact] public void Value_ShouldBeInt() =>
                1.Tree().Value.ShouldBe(1);
        }
    }
}