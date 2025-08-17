using System.Text;

namespace DesignPattern.Flyweight
{
    public class TextFormatFW
    {
        public class FormattedText
        {
            private readonly string _plainText;
            private bool[] capitalize;

            public FormattedText(string plainText)
            {
                this._plainText = plainText;
                capitalize = new bool[plainText.Length];
            }

            public void Capitalize(int start, int end)
            {
                for(int i = start; i < end; i++)
                    capitalize[i] = true;
            }

            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < _plainText.Length; i++)
                {
                    char c = _plainText[i];
                    stringBuilder.Append(capitalize[i] ? char.ToUpper(c) : c);
                }
                return stringBuilder.ToString();
            }
        }

        public class BetterFormattedText
        {
            private string plainText;
            private List<TextRange> formatting = new List<TextRange>();

            public BetterFormattedText(string plainText)
            {
                this.plainText = plainText;
            }

            public TextRange GetRange(int start, int end)
            {
                var range = new TextRange { Start = start, End = end };
                formatting.Add(range);
                return range;
            }

            public class TextRange
            {
                public int Start, End;
                public bool Capitalize, Bold, Italic;

                public bool Covers(int position)
                {
                    return position >= Start && position < End;
                }
            }

            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < plainText.Length; i++)
                {
                    var c = plainText[i];
                    foreach (var range in formatting)
                        if (range.Covers(i) && range.Capitalize)
                            c = char.ToUpper(c);
                    stringBuilder.Append(c);
                }
                return stringBuilder.ToString();
            }
        }

        public void Run()
        {
            FormattedText text = new FormattedText("This is a brave new world");
            text.Capitalize(10, 15);
            Console.WriteLine(text);

            BetterFormattedText betterFormatted = new BetterFormattedText("This is a brave new world");
            betterFormatted.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(betterFormatted);
        }
    }
}
