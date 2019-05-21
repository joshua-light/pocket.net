using System;

namespace Pocket.Common.ObjectTree
{
    public class Node
    {
        internal static Node Of<T>(T value) => Of(typeof(T), value);
        internal static Node Of(Type type, object value) => new Node(type, value);

        internal Node(Type type, object value)
        {
            Type = type;
            Value = value;
        }
        
        public Type Type { get; }
        public object Value { get; }
    }
}