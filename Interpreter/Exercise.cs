using System;
using System.Collections.Generic;
using System.Text; // Needed for StringBuilder

namespace DesignPattern.Interpreter
{
    public class Exercise
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public interface IElement
        {
            int Value { get; }
        }

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
                Addition, Subtraction
            }

            private Type _bOType;
            private IElement _left = null, _right = null;

            public void SetBoType(Type boType) => this._bOType = boType;
            public void SetLeftElement(IElement left) => this._left = left;
            public void SetRightElement(IElement right) => this._right = right;

            public IElement GetLeftElement() => this._left;
            public IElement GetRightElement() => this._right;

            public int Value
            {
                get
                {
                    if (_left == null && _right == null)
                        throw new NullReferenceException("Left and Right element is null");
                    if (_right == null)
                        return _left.Value;

                    switch (_bOType)
                    {
                        case Type.Addition: return _left.Value + _right.Value;
                        case Type.Subtraction: return _left.Value - _right.Value;
                        default: throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public class Token
        {
            public enum Type
            {
                Integer, Alphabet, Plus, Minus
            }

            private readonly Type _type;
            private readonly string _text;

            public Token(string text)
            {
                _text = text;
                _type = IdentifyType();
            }

            public Type GetTokenType() => _type;
            public string GetText() => _text;

            private bool IsAllDigits(string s)
            {
                foreach (char c in s)
                    if (!char.IsDigit(c)) return false;
                return true;
            }

            private bool IsAllLetters(string s)
            {
                foreach (char c in s)
                    if (!char.IsLetter(c)) return false;
                return true;
            }

            public Type IdentifyType()
            {
                if (IsAllDigits(_text)) return Type.Integer;
                else if (IsAllLetters(_text)) return Type.Alphabet;
                else if (_text == "+") return Type.Plus;
                else if (_text == "-") return Type.Minus;

                throw new ArgumentOutOfRangeException();
            }

            public override string ToString() => $"{_text}";
        }

        public List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]) || char.IsLetter(input[i]))
                {
                    string fullText = ExtractText(input, ref i);
                    result.Add(new Token(fullText));
                }
                else
                {
                    result.Add(new Token(input[i].ToString()));
                }
            }
            return result;
        }

        public string ExtractText(string input, ref int i)
        {
            var sb = new StringBuilder(input[i].ToString());
            for (int j = i + 1; j < input.Length; j++)
            {
                if (char.IsDigit(input[j]) || char.IsLetter(input[j]))
                {
                    sb.Append(input[j]);
                    i++;
                }
                else break;
            }
            return sb.ToString();
        }

        public IElement Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                switch (token.GetTokenType())
                {
                    case Token.Type.Integer:
                        var integer = new Integer(int.Parse(token.GetText()));
                        SetElement(ref result, integer);
                        break;

                    case Token.Type.Alphabet:
                        char key = token.GetText()[0];
                        if(token.GetText().Length > 1) return new Integer(0);
                        if (Variables.ContainsKey(key))
                        {
                            var variable = new Integer(Variables[key]);
                            SetElement(ref result, variable);
                        }
                        else return new Integer(0);
                        break;

                    case Token.Type.Plus:
                        result.SetBoType(BinaryOperation.Type.Addition);
                        break;

                    case Token.Type.Minus:
                        result.SetBoType(BinaryOperation.Type.Subtraction);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (result.GetLeftElement() != null && result.GetRightElement() != null)
                {
                    var newResult = new BinaryOperation();
                    newResult.SetLeftElement(result);
                    result = newResult;
                }
            }
            return result;
        }

        public void SetElement(ref BinaryOperation result, IElement element)
        {
            if (result.GetLeftElement() == null)
                result.SetLeftElement(element);
            else
                result.SetRightElement(element);
        }

        public int Calculate(string expression)
        {
            var tokens = Lex(expression);
            IElement elems = Parse(tokens);
            return elems.Value;
        }
    }
}