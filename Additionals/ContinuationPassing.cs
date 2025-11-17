using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Additionals
{
    public class ContinuationPassing
    {
        public class QuadraticEquationSolver
        {
            // ax^2 + bx + c = 0


            public enum WorkflowResult
            {
                Success, Failure
            }

            public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex> result)
            {
                var disc = b * b - 4 * a * c;
                if (disc < 0)
                {
                    return SolveComplex(a, b, disc, out result);
                }
                else
                {
                    return SolveSimple(a, b, disc, out result);
                }
            }

            private WorkflowResult SolveComplex(double a, double b, double disc,
                out Tuple<Complex, Complex> result)
            {
                var rootDisc = Complex.Sqrt(new Complex(disc, 0));
                result = Tuple.Create(
                    (-b + rootDisc) / (2 * a),
                    (-b - rootDisc) / (2 * a)
                );

                return WorkflowResult.Success;
            }

            private WorkflowResult SolveSimple(double a, double b, double disc, out Tuple<Complex, Complex> result)
            {
                var rootDisc = Math.Sqrt(disc);
                result = Tuple.Create(
                    new Complex((-b + rootDisc) / (2 * a), 0),
                    new Complex((-b - rootDisc) / (2 * a), 0)
                );
                return WorkflowResult.Success;
            }
        }

        public class ContinuationPassingStyleDemo
        {
            public static void Run()
            {
                QuadraticEquationSolver solver = new QuadraticEquationSolver();
                Tuple<Complex, Complex> solutions;
                var flag = solver.Start(1, 2, 5, out solutions);
                if(flag == QuadraticEquationSolver.WorkflowResult.Success)
                {
                    Console.WriteLine($"Solutions: {solutions.Item1}, {solutions.Item2}");
                }
                else
                {
                    Console.WriteLine("Failed to solve the equation.");
                }
            }
        }
    }
}
