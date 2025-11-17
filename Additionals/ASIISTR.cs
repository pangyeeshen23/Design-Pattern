using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Additionals
{
    public class ASIISTR
    {
        
        public class str : IEquatable<str>
        {
            [NotNull] protected readonly byte[] _buffer;

            public str()
            {
                _buffer = new byte[] { };
            }

            public str(string s)
            {
                _buffer = Encoding.ASCII.GetBytes(s);
            }

            // str + str

            public static str operator + (str left, str right)
            {
                var bytes = new byte[
                    left._buffer.Length + right._buffer.Length
                ];
                left._buffer.CopyTo(bytes, 0);
                right._buffer.CopyTo(bytes, left._buffer.Length);
                return new str(bytes);
            }

            protected str(byte[] buffer)
            {
                this._buffer = buffer;
            }

            public static implicit operator str(string s)
            {
                return new str(s);
            }

            public char this[int index]
            {
                get => (char) _buffer[index];
                set => _buffer[index] = (byte) value;
            }

            public bool Equals(str other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return ((IStructuralEquatable) _buffer).Equals(other._buffer, 
                    StructuralComparisons.StructuralEqualityComparer);
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((str) obj);
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public static bool operator == (str left, str right)
            {
                return Equals(left, right);
            }
            
            public static bool operator != (str left, str right)
            {
                return !Equals(left, right);
            }

            public override string ToString()
            {
                return Encoding.ASCII.GetString(_buffer);
            }
        }

        public static void Run()
        {
            str a = "Hello, ";
            str b = "World!";

            str c = a + b;

            Console.WriteLine(c.ToString());
        }
    }
}
