using System.Linq;
using Pocket.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Tests.ObjectTree.Nodes
{
    public class PropertyNodeTest
    {
        private class Some
        {
            public int Property => 1;
        }

        [Fact] public void Type_ShouldBeFieldType() =>
            Node().Type.ShouldBe(typeof(int));
        [Fact] public void Value_ShouldBeFieldValue() =>
            Node().Value.ShouldBe(1);
        [Fact] public void PropertyInfo_ShouldBeMatchProperty() =>
            Node().Info.Name.ShouldBe("Property");

        private static PropertyNode Node() =>
            typeof(Some).Properties(of: new Some())
                .First()
                .As(x => new PropertyNode(x.Info, x.Value));
    }
}