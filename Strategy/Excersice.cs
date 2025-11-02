using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Strategy
{
    public static class Excersice
    {
        public interface IDiscriminantStrategy
        {
            double CalculateDiscriminant(double a, double b, double c);
        }

        public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
        {
            // todo
            public double CalculateDiscriminant(double a, double b, double c)
            {
                return Math.Pow(b, 2) - 4 * a * c;
            }
        }

        public class RealDiscriminantStrategy : IDiscriminantStrategy
        {
            // todo (return NaN on negative discriminant!)
            public double CalculateDiscriminant(double a, double b, double c)
            {
                double discriminant = b * b - 4 * a * c;
                if (discriminant < 0) return double.NaN;
                else return discriminant;
            }
        }

        public class QuadraticEquationSolver
        {
            private readonly IDiscriminantStrategy strategy;

            public QuadraticEquationSolver(IDiscriminantStrategy strategy)
            {
                this.strategy = strategy;
            }

            public Tuple<Complex, Complex> Solve(double a, double b, double c)
            {
                double discriminant = this.strategy.CalculateDiscriminant(a, b, c);
                Complex x1 = (-b + Complex.Sqrt(discriminant)) / (2 * a);
                Complex x2 = (-b - Complex.Sqrt(discriminant)) / (2 * a);
                return Tuple.Create(x1, x2);
            }
        }
    }
}
