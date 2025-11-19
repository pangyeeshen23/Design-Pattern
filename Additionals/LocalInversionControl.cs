using Dynamitey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Additionals
{

    public static class ExtensionMethods
    {

        public struct BoolMarker<T>
        {
            public bool Result;
            public T Self;

            public enum Operation
            {
                None,
                And,
                Or
            }

            internal Operation PendingOp;

            internal BoolMarker(bool result, T self, Operation pendingOp)
            {
                Result = result;
                Self = self;
                PendingOp = pendingOp;
            }

            public BoolMarker(bool result, T self) : this(result, self, Operation.None)
            {
                
            }

            public static implicit operator bool(BoolMarker<T> marker)
            {
                return marker.Result;
            }

            public BoolMarker<T> And => new BoolMarker<T>(this.Result, this.Self, Operation.And);

   

        }

        public static T AddTo<T>(this T obj, params ICollection<T>[] colls)
        {
            foreach(var col in colls)
                col.Add(obj);
            return obj;
        }

        public static bool IsOneOf<T>(this T self, params T[] options)
        {
            return options.Contains(self);
        }

        public static bool HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return !props(self).Any();
        }

        public static bool HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return props(self).Any();
        }

        public static BoolMarker<TSubject> HasNoBool<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(!props(self).Any(), self);
        }

        public static BoolMarker<TSubject> HasSomeBool<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(props(self).Any(), self);
        }
        public static BoolMarker<T> HasNo<T, U>(this BoolMarker<T> marker, Func<T, IEnumerable<U>> props)
        {
            if (marker.PendingOp == BoolMarker<T>.Operation.And && !marker.Result) return marker;
            return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
        }
    }

    public class Person
    {
        public List<string> Names = new List<string>();
        public List<Person> Children = new List<Person>();
    }

    public class LocalInversionControl
    {
        public void AddingNumbers()
        {
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            24.AddTo(list, list2);
            42.AddTo(list);
        }

        public void ProcessCommnad(string opcode)
        {
            if(opcode == "AND" || opcode == "OR" || opcode == "XOR")
            {
                
            }

            if (new[] { "AND", "OR", "XOR" }.Contains(opcode))
            {
                Console.WriteLine("Logical operation");
            }

            if ("AND OR XOR".Split(" ").Contains(opcode))
            {
                Console.WriteLine("Logical operation");
            }

            if(opcode.IsOneOf("AND", "OR", "XOR"))
            {
                Console.WriteLine("Logical operation");
            }

        }

        public void Process(Person person)
        {
            if (person.Names.Count == 0)
            {
               
            }

            if(person.Names.Any())
            {

            }

            if(person.HasNo(p => p.Names))
            {
                Console.WriteLine("No names");
            }

            if(person.HasSome(p => p.Names))
            {
                Console.WriteLine("Has children");
            }

            if (person.HasSomeBool(p => p.Names).And.HasNo(p => p.Children))
            {
                Console.WriteLine("Has children");
            }
        }
    }
}
