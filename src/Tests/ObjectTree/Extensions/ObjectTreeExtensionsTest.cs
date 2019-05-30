using System.Collections.Generic;
using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;
using static System.Environment;

namespace Pocket.Common.Tests.ObjectTree.Extensions
{
    public class ObjectTreeExtensionsTest
    {
        private class Simple
        {
            public int X = 1;
            public int Y = 2;
        }

        private class WithNested
        {
            public Simple Simple = new Simple();
        }
        
        private class WithNull
        {
            public Simple Null;
        }
        
        [Fact] public void NodeOf_Null_ShouldBePrintedAs_Null() =>
            Node<string>(of: null).AsText().ShouldBe("null");
        [Fact] public void NodeOf_1_ShouldBePrintedAs_1() =>
            Node(of: 1).AsText().ShouldBe("1");
        [Fact] public void NodeOf_Hello_ShouldBePrintedAs_Hello() =>
            Node(of: "Hello").AsText().ShouldBe("Hello");

        [Fact] public void NodeOf_Tuple_ShouldBePrintedWithRoundBrackets() =>
            Node(of: (1, 1)).AsText().ShouldBe("(1, 1)");

        [Fact] public void NodeOf_List_ShouldBePrintedAsValuesWithSquareBrackets() =>
            Node(of: new List<int> { 1, 2, 3, 4, 5 })
               .AsText().ShouldBe("[ 1, 2, 3, 4, 5 ]");

        [Fact] public void NodeOf_SimpleObject_ShouldBePrintedWithFieldNames() =>
            Node(of: new Simple()).AsText().ShouldBe(
                "X: 1" + NewLine +
                "Y: 2");

        [Fact] public void NodeOf_WithNestedObject_ShouldBePrintedWithIndent() =>
            Node(of: new WithNested()).AsText().ShouldBe(
                "Simple:" + NewLine +
                "    X: 1" + NewLine +
                "    Y: 2");

        [Fact] public void NodeOf_ObjectWithNull_ShouldPrintFieldWithNameAndNullValue() =>
            Node(of: new WithNull()).AsText().ShouldBe(
                "Null: null");
        
        private static Node Node<T>(T of) => of.Tree(typeof(T));
    }
}