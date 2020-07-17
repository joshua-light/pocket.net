using System;
using System.Linq;
using static Pocket.Guard;

namespace Pocket.ObjectTree
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
                    case ObjectNode x: Object(x); break;
                    case CollectionNode x: Collection(x); break;
                    case PrimitiveNode x: Primitive(x); break;
                    case NullNode x: Null(x); break;
                }
            }

            void Object(ObjectNode x)
            {
                if (x.Value == null)
                {
                    Text("null");
                    return;
                }
                
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
                Text($"{x.Info.Name}:");
                Text(" ", when: x.Inner.GetType() != typeof(ObjectNode));
                Node(x.Inner);
                Line(when: !isLast);
            }

            void Property(PropertyNode x, bool isLast = false)
            {
                Text($"{x.Info.Name}:");
                Text(" ", when: x.Inner.GetType() != typeof(ObjectNode));
                Node(x.Inner);
                Line(when: !isLast);
            }

            void Collection(CollectionNode x)
            {
                if (x.Children.IsEmpty())
                    Text($"[]");
                else if (x.Children.First() is ObjectNode)
                {
                    Line(when: !code.IsEmpty);
                    Text("---");
                    foreach (var child in x.Children.OfType<ObjectNode>())
                    {
                        Object(child);
                        Line(when: child != x.Children.Last());
                        Text("---");
                    }
                }
                else
                    Text($"[ {x.Children.Select(AsText).Separated(with: ", ")} ]");
            }
            
            void Primitive(PrimitiveNode x) =>
                Text(x.Value);
            
            void Null(NullNode x) =>
                Text("null");
            
            Code.Scope Indent(int size) =>
                code.Indent(size);
            void Text(object value, bool when = true) =>
                code.Text(value?.ToString() ?? "null", when);
            void Line(bool when = true) =>
                code.NewLine(when);
        }
    }
}