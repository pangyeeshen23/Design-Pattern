using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Builder
{
    class StepwiseBuilder
    {
        // Stepwise Builder Pattern Ensure that the build process is done in a specific order
        public enum CarType
        {
            Sedan,
            Crossover
        }

        public class Car
        {
            public CarType Type { get; set; }
            public int WheelSize { get; set; }

            public override string ToString()
            {
                return $"{nameof(Type)}: {Type}, {nameof(WheelSize)}: {WheelSize}";
            }
        }

        public interface ISpecifyCarType
        {
            public ISpecifyWheelSize OfType(CarType type);
        }

        public interface ISpecifyWheelSize
        {
            public IBuildCar WithWheels(int size);
        }

        public interface IBuildCar
        {
            public Car Build();
        }

        public static class CarBuilder 
        {
            private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
            {
                private Car car = new Car();
                public Car Build()
                {
                    return car;
                }
                public ISpecifyWheelSize OfType(CarType type)
                {
                    car.Type = type;
                    return this;
                }

                public IBuildCar WithWheels(int size)
                {
                    switch(car.Type)
                    {
                        case CarType.Crossover when size < 17 || size > 20:
                        case CarType.Sedan when size < 15 || size > 17:
                                throw new ArgumentException("Crossover wheel size must be between 20 and 22 inches.");
                    }
                    car.WheelSize = size;
                    return this;
                }
            }

            public static ISpecifyCarType Create()
            {
                return new Impl();
            }
        }
    }
}
