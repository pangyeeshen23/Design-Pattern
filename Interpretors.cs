using DesignPattern.Interpreter;

namespace DesignPattern
{
    public class Interpretors
    {
        public void Run()
        {
            //Interpretor interpretor = new Interpretor();
            //interpretor.Run();

            Exercise exercise = new Exercise();
            exercise.Variables['x'] = 3;
            Console.WriteLine(exercise.Calculate("1+2+3"));
        }
    }
}
