using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Singleton
{
    public class AmbientContext
    {
    }

    // This is an example of an ambient context, which is a design pattern that allows you to set a context for a certain scope (like a thread) and access it from anywhere within that scope.
    // so rather thatn passing value that is used by the object, we can set it in the ambient context and the object will use it.

    // Version 1 : Using ThreadStatic
    //public sealed class BuildingContext
    //{
    //    [ThreadStatic]
    //    public static int WallHeight;
    //}

    // Version 2 : Using Scope
    public sealed class BuildingContext : IDisposable
    {
        [ThreadStatic]
        public int WallHeight;
        private static Stack<BuildingContext> stack = new Stack<BuildingContext>();

        static BuildingContext()
        {
            stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int wallHeight)
        {
            WallHeight = wallHeight;
            stack.Push(this);
        }

        public static BuildingContext Current => stack.Peek();

        public void Dispose()
        {
            if (stack.Count > 1) stack.Pop();
        }
    }

    public class Building
    {
        public List<Wall> Walls = new List<Wall>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Wall wall in Walls)
            {
                sb.AppendLine(wall.ToString());
            }
            return sb.ToString();
        }
    }

    public class Point
    {
        private int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)} : {x}, {nameof(y)} : {y}";
        }
    }

    public class Wall
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public int Height { get; set; }

        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight;
        }
        public override string ToString()
        {
            return $"{nameof(Height)} : {Height}, {nameof(Start)} : {Start}, {nameof(End)} : {End}";
        }
    }
}
