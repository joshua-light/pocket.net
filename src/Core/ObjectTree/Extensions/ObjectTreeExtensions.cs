using System;
using System.Linq;
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
                    case CollectionNode x: Collection(x); break;
                    case ObjectNode x: Object(x); break;
                }
            }

            void Primitive(PrimitiveNode x) =>
                Text(x.Value);

            void Collection(CollectionNode x) =>
                Text($"[ {x.Children.Select(AsText).Separated(with: ", ")} ]");

            void Object(ObjectNode x)
            {
                var children = x.Children.ToList();
                
                Line(when: !code.IsEmpty);

                using (Indent(size: code.IsEmpty ? 0 : 4))
                {
                    children
                        .OfType<FieldNode>()
                        .ForEach(y => Field(y, y == children.Last()));
                    children
                        .OfType<PropertyNode>()
                        .ForEach(y => Property(y, y == children.Last()));
                }
            }

            void Field(FieldNode x, bool isLast = false)
            {
                Text($"{x.Info.Name}: ");
                Node(x.Inner);
                Line(when: !isLast);
            }

            void Property(PropertyNode x, bool isLast = false)
            {
                Text($"{x.Info.Name}: ");
                Node(x.Inner);
                Line(when: !isLast);
            }
            
            Code.Scope Indent(int size) =>
                code.Indent(size);
            void Text(object value) =>
                code.Text(value?.ToString() ?? "null");
            void Line(bool when = true) =>
                code.NewLine(when);
        }
    }
}