using System.Collections.Generic;
using System.Linq;
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
            [Fact] public void Value_ShouldBe1() =>
                1.Tree().Value.ShouldBe(1);
        }

        public class EnumObjectNode
        {
            private enum Enum { Value }

            [Fact] public void TypeOfNode_ShouldBePrimitiveNode() =>
                Enum.Value.Tree().ShouldBeOfType<PrimitiveNode>();
            [Fact] public void Type_ShouldBeEnum() =>
                Enum.Value.Tree().Type.ShouldBe(typeof(Enum));
            [Fact] public void Value_ShouldBeEnumValue() =>
                Enum.Value.Tree().Value.ShouldBe(Enum.Value);
        }
        
        public class StringObjectNode
        {
            private const string String = "";

            [Fact] public void TypeOfNode_ShouldBePrimitiveNode() =>
                String.Tree().ShouldBeOfType<PrimitiveNode>();
            [Fact] public void Type_ShouldBeEnum() =>
                String.Tree().Type.ShouldBe(typeof(string));
            [Fact] public void Value_ShouldBeEmptyString() =>
                String.Tree().Value.ShouldBe(String);
        }

        public class EnumerableObjectNode
        {
            private readonly IEnumerable<int> _value = Enumerable.Range(0, 10);
            
            [Fact] public void TypeOfNode_ShouldBePrimitiveNode() =>
                _value.Tree().ShouldBeOfType<CollectionNode>();
            [Fact] public void Type_ShouldBeEnum() =>
                _value.Tree().Type.ShouldBe(typeof(IEnumerable<int>));
            [Fact] public void Value_ShouldBeSequenceOfNumbers() =>
                _value.Tree().Value.ShouldBe(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            [Fact] public void ChildrenCount_ShouldBe10() =>
                _value.Tree().Children.Count().ShouldBe(10);
            [Fact] public void Children_ShouldRepresentSequenceOfNumbers() =>
                _value.Tree().Children.Select(x => x.Value).ShouldBe(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }
}