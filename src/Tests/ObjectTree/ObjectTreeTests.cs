using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pocket.Common.ObjectTree;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.ObjectTree
{
    public class ObjectTreeTests
    {
        [Fact] public void DefaultObject_ShouldBeEmptyNode() =>
            Value(of: new object()).ShouldBeConvertedTo<EmptyNode>();

        [Theory]
        [InlineData(10)]
        [InlineData(10.0)]
        [InlineData("Hello")]
        [InlineData(BindingFlags.Default)]
        public void PrimitiveValues_ShouldBePrimitiveNode(object x) =>
            Value(of: x).ShouldBeConvertedTo<PrimitiveNode>();

        [Theory]
        [InlineData(new [] { 1, 2, 3 })]
        public void CollectionValues_ShouldBeCollectionNode(object x) =>
            Value(of: x).ShouldBeConvertedTo<CollectionNode>();

        [Theory]
        [InlineData(new [] { 1, 2, 3 })]
        public void CollectionValuesChildren_ShouldMatchCollections(object x) =>
            Children(of: x).Select(y => y.Value).ShouldBe(x);
        
        private static IEnumerable<Node> Children(object of) =>
            of.Tree().Children;
        
        private static ValueOf Value(object of) =>
                   new ValueOf(of.GetType(), of);
        
        private class ValueOf
        {
            private readonly Type _type;
            private readonly object _value;

            public ValueOf(Type type, object value)
            {
                _type = type;
                _value = value;
            }

            public void ShouldBeConvertedTo<TNode>() where TNode : Node
            {
                _value.Tree().ShouldBeOfType<TNode>();
                _value.Tree().Type.ShouldBe(_type);
                _value.Tree().Value.ShouldBe(_value);
            }
        }
    }
}