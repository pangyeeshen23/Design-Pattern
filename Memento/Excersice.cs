using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.Memento.Excersice;

namespace DesignPattern.Memento
{
    public class Excersice
    {
        public interface IPrototye<T>
        {
            public T DeepCopy();
        }
        public class Token : IPrototye<Token>
        {
            public int Value = 0;

            public Token(int value)
            {
                this.Value = value;
            }

            public Token DeepCopy()
            {
                return new Token(Value);
            }
        }

        public class Memento
        {
            public List<Token> Tokens { get; } = new List<Token>();
        }

        public class TokenMachine
        {
            public List<Token> Tokens = new List<Token>();
            private List<Memento> _changes = new List<Memento>();

            public Memento AddToken(int value)
            {
                Token token = new Token(value);
                return AddToken(token);
            }

            public Memento AddToken(Token token)
            {
                Tokens.Add(token);
                Memento memento = new Memento();
                foreach(Token tkn in Tokens)
                {
                    memento.Tokens.Add(tkn.DeepCopy());
                }
                _changes.Add(memento);
                return memento;
            }

            public void Revert(Memento m)
            {
                this.Tokens = m.Tokens;
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
