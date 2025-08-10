using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding.Exercises;

namespace DesignPattern.Decorator
{

    //Static Composition 
    class GenericDecorator
    {

        //CRTP
        public abstract class Shape
        {
            public abstract string AsString();
        }

        public class Circle : Shape
        {
            private float radius;
            public Circle() : this(0.0f)
            {

            }

            public Circle(float radius)
            {
                this.radius = radius;
            }

            public void Resize(float factor)
            {
                radius *= factor;
            }

            public override string AsString() => $"A circle of radius {radius}";
        }

        public class Square : Shape
        {
            private float side;

            public Square() : this(0.0f)
            {
                
            }

            public Square(float side)
            {
                this.side = side;
            }

            public override string AsString() => $"A square with side {side}";
        }

        public class ColoredShape : Shape
        {
            private Shape shape;
            private string color;

            public ColoredShape(Shape shape, string color)
            {
                this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
                this.color = color ?? throw new ArgumentNullException(nameof(color));
            }

            public override string AsString() => $"{shape.AsString()} has the color {color}";
        }

        public class TransparentShape : Shape
        {
            private Shape shape;
            private float transparency;

            public TransparentShape(Shape shape, float transparency)
            {
                this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
                this.transparency = transparency;
            }

            public override string AsString() => $"{shape.AsString()} has {transparency * 100.0}%  transparency";
        }

        public class ColoredShape<T> : Shape where T : Shape, new()
        {
            public string color;
            T shape = new T();

            public ColoredShape() : this("black")
            {

            }

            public ColoredShape(string color)
            {
                this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
            }

            public override string AsString() =>  $"{shape.AsString()} has the color {color ?? "no color"}";
        }

        public class TransparentShape<T> : Shape where T : Shape, new ()
        {
            private float transparency;
            private T shape = new T();

            public TransparentShape() : this(0)
            {
                
            }

            public TransparentShape(float transparency)
            {
                this.transparency = transparency;
            }

            public override string AsString() => $"{shape.AsString()} has {transparency * 100.0}%  transparency";
        }

        public void Run()
        {
            ColoredShape<Square> coloredShape = new ColoredShape<Square>("red");
            Console.WriteLine(coloredShape.AsString());

            TransparentShape<ColoredShape<Circle>> transparentShape = new TransparentShape<ColoredShape<Circle>>(0.4f);
            Console.WriteLine(transparentShape.AsString());
        }
    }

}
