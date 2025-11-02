using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Strategy
{
    public enum OutputFormat
    {
        Markdown,
        Html
    }

    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);

        // static strategy
        
    }

    public class HtmlListStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" <li>{item}</li>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }
    }

    public class MarkdownListStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }

        public void End(StringBuilder sb)
        {

        }

        public void Start(StringBuilder sb)
        {

        }
    }

    public class TextProcessor<LS> where LS : IListStrategy, new()
    {
        private StringBuilder sb = new StringBuilder();
        private IListStrategy listStrat = new LS();

        //public void SetOutputFormat(OutputFormat format)
        //{
        //    switch (format)
        //    {
        //        case OutputFormat.Markdown:
        //            listStrat = new MarkdownListStrategy();
        //            break;
        //        case OutputFormat.Html:
        //            listStrat = new HtmlListStrategy();
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(format), format, null);
        //    }
        //}

        public void AppendList(IEnumerable<string> items)
        {
            listStrat.Start(sb);
            foreach (var item in items)
            {
                listStrat.AddListItem(sb, item);
            }
            listStrat.End(sb);
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        public StringBuilder Clear()
        {
            return sb.Clear();
        }

    }

    public static class DynamicNStaticStrategy
    {
        public static void Run()
        {
            //Dynamic Strategy Pattern
            //TextProcessor processor = new TextProcessor();
            //processor.SetOutputFormat(OutputFormat.Html);
            //processor.AppendList(new[] {"foo", "bar", "baz"});
            //Console.WriteLine(processor);

            // Static Strategy Pattern
            TextProcessor<MarkdownListStrategy> text = new TextProcessor<MarkdownListStrategy>();
            text.AppendList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(text);

            TextProcessor<HtmlListStrategy> htmlText = new TextProcessor<HtmlListStrategy>();
            htmlText.AppendList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(htmlText);
        }
    }
}

