using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.ObjectTree
{
    public class CollectionNode : Node
    {
        internal static Node Of(Type type, object value) =>
            type.Is<IEnumerable>() ||
            type.Implements<IEnumerable>()
                ? new CollectionNode(type, value, Nodes(of: (IEnumerable) value)) 
                : null;

        private static IEnumerable<Node> Nodes(IEnumerable of) =>
            from object x in of select x.Tree();
        
        private CollectionNode(Type type, object value, IEnumerable<Node> children = null) : base(type, value, children) { }
    }
}