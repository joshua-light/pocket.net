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

            [Fact] public void Type_ShouldBeObject() =>
                _object.Tree().Type.ShouldBe(typeof(object));
            [Fact] public void Value_ShouldBeSameAsDeclaredObject() =>
                _object.Tree().Value.ShouldBe(_object);
        }
    }
}