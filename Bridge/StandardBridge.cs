using System.ComponentModel;
using Autofac;

namespace DesignPattern.Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a pixels for circle with radius {radius}");
        }
    }


    public abstract class Shape
    {
        protected IRenderer _renderer;
        protected Shape(
            IRenderer renderer
        )
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        protected float _radius;
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            _renderer.RenderCircle(_radius);
        }

        public override void Resize(float factor)
        {
            _radius *= factor;
        }
    }

    public class Square : Shape
    {
        protected float _radius;

        public Square(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;

        }

        public override void Draw() 
        {
        }

        public override void Resize(float factor)
        {
            
        }
    }

    public class StandardBridge
    {
        public static void Run()
        {
            //IRenderer renderer = new VectorRenderer();
            //var circle = new Circle(renderer, 5);
            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();


            ContainerBuilder autoFac = new ContainerBuilder();
            autoFac.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
            autoFac.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));
            using (var container = autoFac.Build())
            {
                Circle circle = container.Resolve<Circle>(
                    new PositionalParameter(0, 5f)
                );
                circle.Draw();
                circle.Resize(2f);

            }
        }
    }
}
