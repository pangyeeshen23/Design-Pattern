using System.Collections;

namespace DesignPattern.Iterator
{
    public class Creature : IEnumerable<int>
    {
        private int[] stats = new int[3];
        public int Strength 
        { 
            get
            {
                return stats[0];
            }

            set
            {
                stats[0] = value;
            }
        }

        public int Agility 
        {
            get
            {
                return stats[1];
            }

            set
            {
                stats[1] = value;
            }
        }

        public int Intelligence 
        { 
            get
            {
                return stats[2];
            }

            set
            {
                stats[2] = value;
            }
        }


        public double AverageStats
        {
            get { return stats.Average(); }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int this[int index]
        {
            get { return stats[index]; }
            set { stats[index] = value; }
        }
    }

    public class ArrayBackProperty
    {
        public void ShowStats()
        {
            Creature creature = new Creature();
            creature.Strength = 10;
            creature.Agility = 5;
            creature.Intelligence = 3;

            foreach(int stat in creature)
            {
                Console.WriteLine(stat);
            }
        }

    }
}
