using System;

namespace Pocket.Common.ObjectTree
{
    public class EmptyNode : Node
    {
        public static Node Of(Type type, object value) =>
            type == typeof(object) ? new EmptyNode(type, value) : null;
        
        private EmptyNode(Type type, object value) : base(type, value) { }
    }
}