namespace DesignPattern.Singleton
{
    // The reason why singleton pattern exist, it because static does not support dependency injection.
    // so it does not allow for different implementations to be used.
    // Static classes are not testable, and they cannot be mocked or stubbed.

    // So here is a thing called monostate
    public class CEO
    {
        private static string name;
        private static int age;

        public string Name 
        { 
            get => name; 
            set => name = value; 
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
