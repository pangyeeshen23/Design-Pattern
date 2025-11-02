using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Template
{

    // Template Design Pattern
    public static class TemplateMethod
    {
        public abstract class Game
        {

            public void Run()
            {
                Start();
                while (!HaveWinner)
                    TakeTurn();
                Console.WriteLine($"Player {WinningPlayer} wins.");
            }

            protected int _currentPlayer;
            protected readonly int _numberOfPlayers;
            protected Game(int numberofPlayers)
            {
                this._numberOfPlayers = numberofPlayers;
            }

            protected abstract void Start();
            protected abstract void TakeTurn();
            protected abstract bool HaveWinner { get; }
            protected abstract int WinningPlayer { get; }
        }

        public class Chess : Game
        {
            private int _turn = 1;
            private int _maxTurn = 10;

            public Chess() : base(2)
            {

            }

            protected override bool HaveWinner => _turn == _maxTurn;

            protected override int WinningPlayer => _currentPlayer;

            protected override void Start()
            {
                Console.WriteLine($"Starting a game of chess with {_numberOfPlayers} players.");
            }

            protected override void TakeTurn()
            {
                Console.WriteLine($"Turn {_turn++} taken by player {_currentPlayer}.");
                _currentPlayer = (_currentPlayer + 1) % _numberOfPlayers;
            }

        }

        public static void Run()
        {
            Chess chess = new Chess();
            chess.Run();
        }
    }
}
