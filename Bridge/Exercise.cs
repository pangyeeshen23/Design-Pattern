using System;

namespace Coding.Exercises
{
    public abstract class Shape
    {
        protected IRenderer _renderer;


        public Shape(IRenderer render)
        {
            _renderer = render;
        }
        public string Name { get; set; }

        public override string ToString()
        {
            string result = _renderer.WhatToRenderAs.Replace("{Name}", this.Name);
            return result;
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {

        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }

    }

    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }


    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "Drawing {Name} as lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "Drawing {Name} as pixels";
    }
}

