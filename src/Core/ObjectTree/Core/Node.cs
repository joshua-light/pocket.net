using System;

namespace Pocket.Common.ObjectTree
{
    public abstract class Node
    {
        internal static Node Of<T>(T value) => Of(typeof(T), value);
        internal static Node Of(Type type, object value) =>
            EmptyNode.Of(type, value) ??
            PrimitiveNode.Of(type, value);

        protected Node(Type type, object value)
        {
            Type = type;
            Value = value;
        }
        
        public Type Type { get; }
        public object Value { get; }
    }
}