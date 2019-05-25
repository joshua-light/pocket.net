using System;
using System.Collections.Generic;

namespace Pocket.Common.ObjectTree
{
    public abstract class Node
    {
        internal static Node Of(Type type, object value) =>
            NullNode.Of(type, value) ??
            EmptyNode.Of(type, value) ??
            PrimitiveNode.Of(type, value) ??
            CollectionNode.Of(type, value) ??
            ObjectNode.Of(type, value);

        protected Node(Type type, object value, IEnumerable<Node> children = null)
        {
            Type = type;
            Value = value;
            Children = children.OrEmpty();
        }
        
        public Type Type { get; }
        public object Value { get; }
        public IEnumerable<Node> Children { get; }
    }
}