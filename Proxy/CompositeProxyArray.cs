using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Proxy
{
    public class CompositeProxyArray
    {
        public class MasonrySettings
        {

            public bool? All
            {
                get
                {
                    if (flags.All(f => f == true))
                        return true;
                    if (flags.Any(f => f == false))
                        return false;
                    return null;
                }

                set
                {
                    if(!value.HasValue) return;
                    for(int i = 0; i < flags.Length; i++)
                    {
                        flags[i] = value.Value;
                    }
                }
            }

            //public bool? All
            //{
            //    get
            //    {
            //        if (Pillars == Walls && Walls == Floors)
            //            return Pillars;
            //        return null;
            //    }
            //    set
            //    {
            //        if (!value.HasValue) return;
            //        Pillars = value.Value;
            //        Walls = value.Value;
            //        Floors = value.Value;
            //    }
            //}

            //public bool Pillars, Walls, Floors;

            private bool[] flags = new bool[3];

            public bool Pillars
            {
                set => flags[0] = value;
                get => flags[0];
            }

            public bool Walls
            {
                set => flags[1] = value;
                get => flags[1];
            }

            public bool Floors
            {
                set => flags[2] = value;
                get => flags[2];
            }
        }

        public void Run()
        {
            MasonrySettings settings = new MasonrySettings();
            settings.All = true;
            Console.WriteLine($"{settings.Pillars} {settings.Walls} {settings.Floors}");
            settings.Walls = false;
            Console.WriteLine($"{settings.Pillars} {settings.Walls} {settings.Floors}");
            Console.WriteLine(settings.All.HasValue ? settings.All.ToString() : "null");
            settings.All = false;
            Console.WriteLine($"{settings.Pillars} {settings.Walls} {settings.Floors}");
        }
    }
}
