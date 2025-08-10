using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Decorator
{
    class DecoratorExersice
    {
        public class Bird
        {
            public int Age { get; set; }

            public string Fly()
            {
                return (Age < 10) ? "flying" : "too old";
            }
        }

        public class Lizard
        {
            public int Age { get; set; }

            public string Crawl()
            {
                return (Age > 1) ? "crawling" : "too young";
            }
        }

        public class Dragon
        {
            private Bird _bird;
            private Lizard _lizard;

            public Dragon() : this(new Bird(), new Lizard())
            {

            }

            public Dragon(Bird bird, Lizard lizard)
            {
                _bird = bird;
                _lizard = lizard;
            }

            public int Age
            {
                get { return Age; }
                set
                {
                    this._bird.Age = value;
                    this._lizard.Age = value;
                }
            }

            public string Fly()
            {
                return _bird.Fly();
            }

            public string Crawl()
            {
                return _lizard.Crawl();// todo
            }
        }

        public void Run()
        {
            Dragon dragon = new Dragon();
            dragon.Age = 5;
            Console.WriteLine(dragon.Fly());   // Output: flying
            Console.WriteLine(dragon.Crawl()); // Output: crawling
            dragon.Age = 15;
            Console.WriteLine(dragon.Fly());   // Output: too old
            Console.WriteLine(dragon.Crawl()); // Output: crawling
            dragon.Age = 0;
            Console.WriteLine(dragon.Fly());   // Output: too old
            Console.WriteLine(dragon.Crawl()); // Output: too young
        }
    }
}
