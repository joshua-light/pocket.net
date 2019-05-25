using System.Linq;
using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.ObjectTree.Nodes
{
    public class ObjectNodeTest
    {
        private class Type
        {
            private int X = 1;
            protected string Y = "2";
            public long Z = 3;

            private int A { get; } = 4;
            private int B { get; set; } = 5;
            private int C => 6;
        }
        
        [Fact] public void Of_ShouldReturnNotNull_IfValueIsObject() =>
            Node<object>(of: new object()).ShouldNotBeNull();

        [Fact] public void Children_ShouldEqualToFieldsAndPropertiesOfType() =>
            Node<Type>(of: new Type())
                .Do(x => x.Children.ToArray()[0].Value.ShouldBe(1))
                .Do(x => x.Children.ToArray()[1].Value.ShouldBe("2"))
                .Do(x => x.Children.ToArray()[2].Value.ShouldBe(3))
                .Do(x => x.Children.ToArray()[3].Value.ShouldBe(4))
                .Do(x => x.Children.ToArray()[4].Value.ShouldBe(5))
                .Do(x => x.Children.ToArray()[5].Value.ShouldBe(6));
        
        private static Node Node<T>(object of) =>
            ObjectNode.Of(typeof(T), of);
    }
}