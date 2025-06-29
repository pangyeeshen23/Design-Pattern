using System.Text;

namespace DesignPattern.Builder
{
    public class CodeBuilder
    {
        private readonly ClassElement classRoot = new ClassElement();

        public CodeBuilder(string className)
        {
            if (className == string.Empty) throw new ArgumentNullException(paramName: nameof(className));
            classRoot.Name = className;
        }

        public CodeBuilder AddField(string fieldName, string type)
        {
            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(type))
                throw new ArgumentException("Field name and data type cannot be null or empty.");
            FieldElement fieldElement = new FieldElement();
            fieldElement.Name = fieldName;
            fieldElement.Type = type;
            classRoot.FieldElements.Add(fieldElement);
            return this;
        }

        public override string ToString()
        {
            return classRoot.ToString();
        }
    }
    public class ClassElement
    {
        public string Name { get; set; }
        public List<FieldElement> FieldElements { get; set; } = new List<FieldElement>();
        private int spaceIndentSize = 1;

        private string ToStringImp()
        {
            StringBuilder sb = new StringBuilder();
            string space = new string(' ', spaceIndentSize);
            string name = char.ToUpper(Name[0]) + Name.Substring(1);
            sb.AppendLine("public" + space + "class" + space + Name);
            sb.AppendLine("{");
            foreach (var field in FieldElements)
            {
                sb.AppendLine(field.ToString());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImp();
        }

    }

    public class FieldElement
    {
        public string Name { get; set; }
        public string Type { get; set; }

        private int startingIndentSize = 4;
        private int spaceIndentSize = 1;

        private string ToStringImpl()
        {
            StringBuilder sb = new StringBuilder();
            var start = new string(' ', startingIndentSize);
            var space = new string(' ', spaceIndentSize);   
            string name = char.ToUpper(Name[0]) + Name.Substring(1);
            sb.Append(start + "public" + space + Type.ToLower() + space + name +";");
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToStringImpl();
        }
    }
}
