using DesignPattern.Composite;

namespace DesignPattern
{
    class Composites
    {
        public void Run()
        {
            GraphicObject draw = new GraphicObject{ Name = "My Drawing" };
            draw.Children.Add(new Square { Color = "Red" });
            draw.Children.Add(new Circle { Color = "Yellow" });
            
            GraphicObject group = new GraphicObject { Name = "Group" };
            group.Children.Add(new Circle { Color = "Blue" });
            group.Children.Add(new Square { Color = "Blue" });
            draw.Children.Add(group);

            draw.Children.Add(new Circle { Color = "Yellow" });


            Console.WriteLine(draw);
        }
    }
}
