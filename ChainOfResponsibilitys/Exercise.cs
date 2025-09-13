namespace DesignPattern.ChainOfResponsibilitys
{
    class Exercise
    {
        public abstract class Creature
        {
            public abstract int Attack { get; }
            public abstract int Defense { get; }
        }

        public class Goblin : Creature
        {
            protected Game _game;
            protected int _baseAttack = 1;
            protected int _baseDefense = 1;
            public Goblin(Game game)
            {
                this._game = game;
            }

            public override int Attack
            {
                get
                {
                    CreatureModifier cm = new CreatureModifier(this);
                    for (int i = 0; i < _game.Creatures.Where(e => e is GoblinKing && e != this).Count(); i++)
                    {
                        cm.Add(new AttackBoostModifier(this));
                    }
                    int result = cm.Handle(this._baseAttack);
                    return result;
                }
            }

            public override int Defense
            {
                get
                {
                    CreatureModifier cm = new CreatureModifier(this);
                    for (int i = 0; i < _game.Creatures.Where(e => (e is Goblin || e is GoblinKing) && e != this).Count(); i++)
                    {
                        cm.Add(new DefenseBoostMofidier(this));
                    }
                    int reuslt = cm.Handle(this._baseDefense);
                    return reuslt;
                }
            }
        }

        public class GoblinKing : Goblin
        {
            public GoblinKing(Game game) : base(game)
            {
                _game = game;
                _baseAttack = 3;
                _baseDefense = 3;
            }
        }

        public class CreatureModifier
        {
            protected Creature creature;
            protected CreatureModifier next;

            public CreatureModifier(Creature creature)
            {
                this.creature = creature;
            }

            public virtual int Handle(int current)
            {
                return next?.Handle(current) ?? current;
            }

            public void Add(CreatureModifier cm)
            {
                if (next != null) next.Add(cm);
                else next = cm;
            }
        }

        public class DefenseBoostMofidier : CreatureModifier
        {
            public DefenseBoostMofidier(Creature creature) : base(creature)
            {
            }

            public override int Handle(int current)
            {
                current += 1;
                return base.Handle(current);
            }
        }

        public class AttackBoostModifier : CreatureModifier
        {
            public AttackBoostModifier(Creature creature) : base(creature)
            {
            }

            public override int Handle(int current)
            {
                current += 1;
                return base.Handle(current);
            }
        }


        public class Game
        {
            public IList<Creature> Creatures = new List<Creature>();
        }

        public void Run()
        {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);
            Console.WriteLine(goblin.Attack);
            Console.WriteLine(goblin.Defense);
        }
    }
}
