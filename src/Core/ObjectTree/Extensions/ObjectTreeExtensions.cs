using System;
using static Pocket.Common.Guard;

namespace Pocket.Common.ObjectTree
{
    public static class ObjectTreeExtensions
    {
        public static Node Tree(this object self, Type of) => 
                      Node.Of(of, self);

        public static Node Tree(this object self)
        {
            Ensure(that: self).NotNull();

            return Node.Of(self.GetType(), self);
        }

        public static string AsText(this Node self)
        {
            var code = new Code();

            Node(self);
            
            return code.ToString();
            
            void Node(Node node)
            {
                switch (node)
                {
                    case PrimitiveNode x: Primitive(x); break;
                }
            }

            void Primitive(PrimitiveNode x) => Text(x.Value);

            Code.Scope Indent() =>
                code.Indent(size: 4);
            void Text(object value) =>
                code.Text(value?.ToString() ?? "null");
        }
    }
}