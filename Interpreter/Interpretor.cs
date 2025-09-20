using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Interpreter
{
    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, LParen, RParen
        }

        public Type MyType;
        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        public override string ToString()
        {
            return $"{Text}";
        }
    }

    public class Interpretor
    {

        public List<Token> Lex(string input)
        {
            List<Token> result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(Token.Type.LParen, "("));
                        break;
                    case ')':
                        result.Add(new Token(Token.Type.RParen, ")"));
                        break;
                    default:
                        string fullInt = ExtractNumber(input, ref i);
                        result.Add(new Token(Token.Type.Integer, fullInt));
                        break;
                }
            }
            return result;
        }

        public string ExtractNumber(string input, ref int i)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(input[i]);
            for (int j = i + 1; j < input.Length; j++)
            {
                if (char.IsDigit(input[j]))
                {
                    sb.Append(input[j]);
                    i++;
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }

        public void Run()
        {
            string input = "(13+4)-(12+1)";
            List<Token> tokens = Lex(input);
            Console.WriteLine(string.Join("\t", tokens));
        }
    }
}
