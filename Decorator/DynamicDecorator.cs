using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding.Exercises;

namespace DesignPattern.Decorator
{
    public class DynamicDecorator
    {
        public interface IShape
        {
            string AsString();

        }

        public class Cicle : IShape
        {
            private float _radius;

            public Cicle(float radius)
            {
                _radius = radius;
            }

            public void Resize(float factor)
            {
                this._radius *= factor;
            }

            public string AsString() => $"A circle with radius {_radius}";
        }

        public class Square : IShape
        {
            private float _side;

            public Square(float side)
            {
                this._side = side;
            }

            public string AsString() => $"A square with side {_side}";
        }

        public class ColorShape : IShape
        {
            private IShape shape;
            private string color;

            public ColorShape(IShape shape, string color)
            {
                this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
                this.color = color ?? throw new ArgumentNullException(nameof(color));
            }

            public string AsString() => $"{shape.AsString()} has the color {color}";
        }

        public class TransparentShape : IShape
        {
            private IShape _shape;
            private float _transparency;

            public TransparentShape(IShape shape, float transparency)
            {
                this._shape = shape ?? throw new ArgumentNullException(nameof(shape));
                this._transparency = transparency;
            }

            public string AsString() => $"{_shape.AsString()} has {_transparency * 100.0}%  transparency";
        }

        public void RunProcess()
        {
            Square square = new Square(1.23f);
            Console.WriteLine(square.AsString());

            ColorShape cs = new ColorShape(square, "red");
            Console.WriteLine(cs.AsString());

            TransparentShape redHalfTransparent = new TransparentShape(cs, 0.5f);
            Console.WriteLine(redHalfTransparent.AsString());

        }
    }
}
