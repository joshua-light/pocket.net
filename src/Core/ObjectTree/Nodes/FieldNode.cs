using System.Reflection;

namespace Pocket.ObjectTree
{
    public class FieldNode : Node
    {
        public FieldNode(FieldInfo field, object value) : base(field.FieldType, value)
        {
            Info = field;
            Inner = Of(field.FieldType, value);
        }

        public FieldInfo Info { get; }
        public Node Inner { get; }
    }
}