namespace DesignPattern.ChainOfResponsibilitys
{

    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier? next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null)
                next.Add(cm);
            else
                next = cm;
        }

        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack ");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDeferenseModifier : CreatureModifier
    {
        public IncreaseDeferenseModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defence");
            creature.Defense += 3;
            base.Handle();
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }
        public override void Handle()
        {
            Console.WriteLine($"No bonuses for {creature.Name}");
        }
    }

    public class MethodChain
    {
        public void Run()
        {
            Creature goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);
            CreatureModifier root = new CreatureModifier(goblin);
            root.Add(new NoBonusesModifier(goblin));
            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new IncreaseDeferenseModifier(goblin));
            root.Handle();
            Console.WriteLine(goblin);
        }
    }
}
