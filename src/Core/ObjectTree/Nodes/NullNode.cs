using System;
using System.Collections.Generic;

namespace Pocket.ObjectTree
{
    public class NullNode : Node	
    {	
        public static Node Of(Type type, object value) =>	
            value == null ? new NullNode(type, value) : null;	

         public NullNode(Type type, object value, IEnumerable<Node> children = null) : base(type, value, children) { }	
    }
}