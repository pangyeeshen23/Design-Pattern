using System.Text;

namespace DesignPattern.Decorator
{
    // this is an example of decorator, where we added new feature 
    public class CodeBuilder
    {
        private StringBuilder _stringBuilder = new StringBuilder();

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }

        public CodeBuilder AppendLine(string line)
        {
            _stringBuilder.AppendLine(line);
            return this;
        }
        
        public CodeBuilder Clear()
        {
            _stringBuilder.Clear();
            return this;
        }
    }
}
