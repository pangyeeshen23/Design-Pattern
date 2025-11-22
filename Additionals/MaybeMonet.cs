using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Additionals
{
    public static class MaybeExtensions
    {
        public static TResult? With <TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator) 
            where TInput : class
            where TResult : class
        {
            if (o == null) return null;
            else return evaluator(o);
        }

        public static TInput? If <TInput>(this TInput o, Func<TInput, bool> evaluator) 
            where TInput : class
        {
            if (o == null) return null;
            else return evaluator(o) ? o : null;
        }

        public static TInput? Do <TInput>(this TInput o, Action<TInput> action) 
            where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if (o == null) return failureValue;
            else return evaluator(o);
        }

        public static TResult WithValue<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TInput : struct
            where TResult : struct
        {
            return evaluator(o);
        }

    }

    public class MaybeMonet
    {

        public class Person
        {
            public Address Address { get; set; }
        }

        public class Address
        {
            public string PostCode { get; set; }
        }

        public class MaybeMonadDemo
        {
            public void MyMethod(Person p)
            {
                // this is quite away from maybe monad style
                //string postCode;
                //if(p != null)
                //{
                //    if(HasMedicalRecord(p) && p.Address != null)
                //    {
                //        CheckAddress(p.Address);
                //        if (p.Address.PostCode != null)
                //        {
                //            postCode = p.Address.PostCode;
                //        }
                //        else
                //        {
                //            postCode = "UNKNOWN";
                //        }
                //    }
                //}

                string postCode = p.With(x => x.Address).With(x => x.PostCode);
                postCode = p
                    .If(x => HasMedicalRecord(x))
                    .With(x => x.Address)
                    .Do(x => CheckAddress(x))
                    .Return(x => x.PostCode, "UNKNOWN");
            }

            public bool HasMedicalRecord(Person p)
            {
                throw new NotImplementedException();
            }

            public bool CheckAddress(Address address)
            {
                return true;
            }

            public static void Run()
            {

            }
        }

    }
}
