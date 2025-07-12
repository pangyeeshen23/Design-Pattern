using System.Text.Json;

namespace DesignPattern.Prototype
{
    public static class SerializeExtension
    {
        public static T DeepCopy<T>(this T self)
        {
            string serialized = JsonSerializer.Serialize(self);
            return JsonSerializer.Deserialize<T>(serialized);
        }
    }
    public class SerializationPrototype
    {

        public class Person
        {
            public string[] Name { get; set; }
            public Address Address { get; set; }

            public override string ToString()
            {
                return $"Name: {string.Join(" ", Name)}, Address : {Address.ToString()}";
            }
        }

        public class Address
        {
            public string StreetName { get; set; }
            public int HouseNumber { get; set; }

            public override string ToString()
            {
                return $"StreetName: {StreetName}, HouseNumber : {HouseNumber}";
            }
        }
    }
}
