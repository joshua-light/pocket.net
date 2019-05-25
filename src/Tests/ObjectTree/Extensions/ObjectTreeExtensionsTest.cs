using System.Collections.Generic;
using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.ObjectTree.Extensions
{
    public class ObjectTreeExtensionsTest
    {
        private class Simple
        {
            public int X = 1;
            public int Y = 2;
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
                "X: 1\r\n" +
                "Y: 2");
        
        private static Node Node<T>(T of) => of.Tree(typeof(T));
    }
}