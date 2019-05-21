using System;
using System.Collections.Generic;

namespace Pocket.Common.ObjectTree
{
    public abstract class Node
    {
        internal static Node Of<T>(T value) => Of(typeof(T), value);
        internal static Node Of(Type type, object value) =>
            EmptyNode.Of(type, value) ??
            PrimitiveNode.Of(type, value);

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