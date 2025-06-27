using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Solid
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Shape
    {

        public Square()
        {

        }
        public Square(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Shape 
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public abstract class Area();
    }

    public static class LiskovSubsitution
    {
        public static int Area(Shape shape)
        {
            return shape.Width * shape.Height;
        }

        public static void MainProcess()
        {
            Rectangle shape = new Rectangle(2, 3);
            Console.WriteLine($"{shape} has area {Area(shape)}");

            Square sq = new Square(2, 3);
            Console.WriteLine($"{shape} has area {Area(shape)}");
        }
    }
}
