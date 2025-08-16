using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Flyweight
{
    // Is to avoid the redundancy when stroring data.
    public class DefaultFW
    {
        public class User
        {
            private string fullName;
            public User(string fullName)
            {
                if(fullName == null)
                {
                    throw new ArgumentNullException(paramName: nameof(fullName));
                }
                this.fullName = fullName;
            }
        }

        public class OptimizedUser
        {
            static List<string> strings = new List<string>();
            private int[] names;

            public OptimizedUser(string fullName)
            {
                int getOrAdd(string s)
                {
                    int idx = strings.IndexOf(s);
                    if (idx != -1) return idx;
                    else
                    {
                        strings.Add(s);
                        return strings.Count - 1;
                    }
                }

                names = fullName.Split(' ').Select(getOrAdd).ToArray();
            }

            public string FullName => string.Join(" ", names.Select(i => strings[i]));
        }

        public void Run()
        {
            
        }
    }
}
