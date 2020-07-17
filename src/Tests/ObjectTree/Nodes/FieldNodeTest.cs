using System.Linq;
using Pocket.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Tests.ObjectTree.Nodes
{
    public class FieldNodeTest
    {
        private class Some
        {
            public int Field;
        }

        [Fact] public void Type_ShouldBeFieldType() =>
            Node().Type.ShouldBe(typeof(int));
        [Fact] public void Value_ShouldBeFieldValue() =>
            Node().Value.ShouldBe(1);
        [Fact] public void FieldInfo_ShouldBeMatchField() =>
            Node().Info.Name.ShouldBe("Field");

        private static FieldNode Node() =>
            typeof(Some).Fields(of: new Some { Field = 1 })
                .First()
                .As(x => new FieldNode(x.Info, x.Value));
    }
}