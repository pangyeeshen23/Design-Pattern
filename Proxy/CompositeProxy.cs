using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Proxy
{
    public class CompositeProxy
    {

        // Stored Method
        // Age, X, Y , Age, X, Y, Age, X, Y
        class Creature
        {
            public byte Age;
            public int X, Y;
        }

        // The reason why we have this class is to improve performance for modern cpus
        // by storing data in arrays instead of individual objects

        // This method of storing is better than the creature class
        // Age, Age, Age, Age
        // X, X, X, X
        // Y, Y, Y, Y
        public class Creatures
        {
            public readonly int size;
            public byte[] ages;
            public byte[] x, y;

            public Creatures(int size)
            {
                this.size = size;
                ages = new byte[size];
                x = new byte[size];
                y = new byte[size];
            }
        }

        public struct CreatureProxy
        {
            private readonly Creatures creatures;
            private readonly int index;
            public CreatureProxy(Creatures creatures, int index)
            {
                this.creatures = creatures;
                this.index = index;
            }

            public ref byte Age => ref creatures.ages[index];
            public ref byte X => ref creatures.x[index];
            public ref byte Y => ref creatures.y[index];

            public IEnumerable<CreatureProxy> GetEnumerator()
            {
                for(int pos = 0; pos < creatures.size; ++pos)
                {
                    yield return new CreatureProxy(creatures, pos);
                }
            }
        }

        public void Run()
        {
            var creatures = new Creature[100];
            for(int i = 0; i < creatures.Length; i++)
            {
                creatures[i] = new Creature();
                creatures[i].X++;
            }
        }

        public void RunOptimized()
        {
            Creatures creatures2 = new Creatures(100);
            CreatureProxy proxy = new CreatureProxy(creatures2, 0);
            foreach (CreatureProxy pr in proxy.GetEnumerator())
            {
                pr.X++;
            }
        }
    }
}
