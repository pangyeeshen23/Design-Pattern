namespace DesignPattern.Proxy
{

    public class ProtectionProxy
    {
        public interface ICar
        {
            void Drive();

        }

        public class Car : ICar
        {
            public void Drive()
            {
                System.Console.WriteLine("Driving the car.");
            }
        }

        public class Driver
        {
            public int Age { get; set; }

            public Driver(int age)
            {
                Age = age; 
            }
        }

        public class CarProxy : ICar
        {
            private Driver driver;
            private Car car = new Car();
            public CarProxy(Driver driver)
            {
                this.driver = driver;
            }

            public void Drive()
            {
                if(driver.Age >= 16)
                {
                    car.Drive();
                }
                else
                {
                    Console.WriteLine("Too Young");
                }
            }
        }
      
        public void Run()
        {
            ICar car = new CarProxy(new Driver(22));
            car.Drive();
        }

    }
}
