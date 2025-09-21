using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Interpreter
{

    public interface IElement
    {
        public int Value { get; }
    }

    // 14
    public class Integer : IElement
    {
        public Integer(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class BinaryOperation : IElement
    {

        public enum Type
        {
            Addition, Subtraction, Multiplication, Division
        }

        public Type MyType;

        public IElement Left, Right;

        public int Value
        {
            get
            {
                switch(MyType)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;
                    case Type.Subtraction:
                        return Left.Value - Right.Value;
                    case Type.Multiplication:
                        return Left.Value * Right.Value;
                    case Type.Division:
                        return Left.Value / Right.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }

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

        public IElement Parse(IReadOnlyList<Token> tokens)
        {
            BinaryOperation result = new BinaryOperation();
            bool haveLHS = false;
            for(int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                switch (token.MyType)
                {
                    case Token.Type.Integer:
                        Integer integer = new Integer(int.Parse(token.Text));
                        setElement(ref result, integer, ref haveLHS);
                        break;
                    case Token.Type.Plus:
                        result.MyType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.MyType = BinaryOperation.Type.Subtraction;
                        break;
                    case Token.Type.LParen:
                        List<Token> subExpression = ExtractSubExpression(ref i, tokens);
                        var element = Parse(subExpression);
                        setElement(ref result, element, ref haveLHS);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return result;
        }

        public List<Token> ExtractSubExpression(ref int startPositision, IReadOnlyList<Token> tokens)
        {
            int endPosition = startPositision;
            for (; endPosition < tokens.Count; ++endPosition)
                if (tokens[endPosition].MyType == Token.Type.RParen)
                    break;
            List<Token> subExpression = tokens.Skip(startPositision + 1).Take(endPosition - startPositision - 1).ToList();
            startPositision = endPosition; // because we had extract the sub expression here. we would skip the entire sub expression for the next loop
            return subExpression;
        }

        public void setElement(ref BinaryOperation result, IElement element, ref bool haveLHS)
        {
            if (!haveLHS)
            {
                result.Left = element;
                haveLHS = true;
            }
            else
            {
                result.Right = element;
            }
        }

        public void Run()
        {
            string input = "(13+4)-(12+1)";
            List<Token> tokens = Lex(input);
            Console.WriteLine(string.Join("\t", tokens));

            IElement elems = Parse(tokens);
        }
    }
}
