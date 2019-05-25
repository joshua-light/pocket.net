using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Pocket.Common.ObjectTree
{
    public class ObjectNode : Node
    {
        public static Node Of(Type type, object value)
        {
            var fields = type
                .Fields(of: value, that: _ => _.AllInstance())
                .Where(x => !x.Info.Has<CompilerGeneratedAttribute>())
                .Select(x => Node.Of(x.Info.FieldType, x.Value));
            var properties = type
                .Properties(of: value, that: _ => _.AllInstance())
                .Select(x => Node.Of(x.Info.PropertyType, x.Value));
            
            return new ObjectNode(type, value, fields.Concat(properties).ToList());
        }
        
        public ObjectNode(Type type, object value, IEnumerable<Node> children = null) : base(type, value, children) { }
    }
}