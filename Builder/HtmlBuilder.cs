using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Builder
{
    class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();
        public HtmlBuilder(string rootName)
        {
            if (rootName == null) throw new ArgumentNullException(paramName: nameof(rootName));
            this.rootName = rootName;
            root.Name = rootName;
        }

        // Fluent Interface. This allow us to chain calls by returning the same object.
        public HtmlBuilder AddChild(string name, string text)
        {
            HtmlElement elem = new HtmlElement(name, text);
            root.Elements.Add(elem);
            return this; // - this return the same object.
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }

    class HtmlElement
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public List<HtmlElement> Elements { get; set; } = new List<HtmlElement>();
        private const int indentSize = 2;
        public HtmlElement()
        {
            
        }
        public HtmlElement(string name, string text)
        {
            if(name == null) throw new ArgumentNullException(paramName: nameof(name));
            if(text == null) throw new ArgumentNullException(paramName: nameof(text));
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * indent + 1));
                sb.AppendLine(Text);
            }

            foreach (var element in Elements)
            {
                sb.Append(element.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
}
