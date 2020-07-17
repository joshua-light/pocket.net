using System.Reflection;

namespace Pocket.ObjectTree
{
    public class PropertyNode : Node
    {
        public PropertyNode(PropertyInfo property, object value) : base(property.PropertyType, value)
        {
            Info = property;
            Inner = Of(property.PropertyType, value);
        }

        public PropertyInfo Info { get; }
        public Node Inner { get; }
    }
}