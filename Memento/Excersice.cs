using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Memento
{
    public class Excersice
    {
        public class Token
        {
            public int Value = 0;

            public Token(int value)
            {
                this.Value = value;
            }
        }

        public class Memento
        {
            public List<int> Values { get; private set; }
            public List<Token> Tokens { get; private set; }

            public Memento(List<int> values, List<Token> tokens)
            {
                Values = values;
                Tokens = tokens;
            }
        }

        public class TokenMachine
        {
            public List<Token> Tokens = new List<Token>();

            public Memento AddToken(int value)
            {
                Token token = new Token(value);
                return AddToken(token);
            }

            public Memento AddToken(Token token)
            {
                Tokens.Add(token);
                List<int> values = new List<int>();
                List<Token> tokens = new List<Token>();
                foreach (Token t in Tokens)
                {
                    values.Add(t.Value);
                    tokens.Add(t);
                }
                Memento memento = new Memento(values, tokens);
                return memento;
            }

            public void Revert(Memento m)
            {
                if (m != null && m.Values != null)
                {
                    Tokens = m.Tokens;
                    for (int i = 0; i < m.Values.Count; i++)
                    {
                        Tokens[i].Value = m.Values[i];
                    }
                }
            }
        }

        public void Run()
        {
            TokenMachine tokenMachine = new TokenMachine();
            var m1 = tokenMachine.AddToken(5);
            tokenMachine.Tokens[0].Value = 333;
            var m2 = tokenMachine.AddToken(10);
            var m3 = tokenMachine.AddToken(15);
            Console.WriteLine($"Tokens count before revert: {tokenMachine.Tokens.Count}");
            tokenMachine.Revert(m1);
            Console.WriteLine($"Tokens count after revert: {tokenMachine.Tokens.Count}");
            tokenMachine.Revert(m2);

        }
    }
}
