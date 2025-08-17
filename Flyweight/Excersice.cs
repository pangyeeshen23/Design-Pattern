using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Flyweight
{
    public class Sentence
    {
        private readonly string[] _words;
        private Dictionary<int, WordToken> _tokens = new Dictionary<int, WordToken>();
        public Sentence(string plainText)
        {
            _words = plainText.Split(" ");
        }

        public WordToken this[int index]
        {
            get
            {
                if (!_tokens.ContainsKey(index))
                    _tokens[index] = new WordToken();
                return _tokens[index];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _words.Length; i++)
            {
                var word = _words[i];
                if(_tokens.ContainsKey(i) && _tokens[i].Capitalize)
                    word = word.ToUpper();
                sb.Append(word);
                if (i < _words.Length - 1)
                    sb.Append(" ");
            }
            return sb.ToString();
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }
}
