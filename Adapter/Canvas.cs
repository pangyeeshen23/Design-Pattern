using System.Collections;
using System.Collections.ObjectModel;
using MoreLinq;

namespace DesignPattern.Adapter
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            if (start == null) throw new ArgumentNullException(paramName: nameof(start));
            if(end == null) throw new ArgumentNullException(paramName: nameof(end));
            Start = start;
            End = end;
        }
        protected bool Equals(Line other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Line)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Start.GetHashCode() * 397) ^ End.GetHashCode();
            }
        }
    }

    public class VectorObject : Collection<Line>
    {

    }
    
    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y))); // Top Line
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height))); // Right Line
            Add(new Line(new Point(x, y), new Point(x, y + height))); // Left Line
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height))); // Bottom Line
        }
    }

    // Adapter for converting Lines to Points, Which is an adapter pattern
    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int count;

        private static Dictionary<int, List<Point>> cache = new Dictionary<int, List<Point>>();

        public LineToPointAdapter(Line line)
        {

            var hash = line.GetHashCode();
            if (cache.ContainsKey(hash)) return;
            Console.Write($"{++count}: Generating points for [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");
            List<Point> points = new List<Point>(); 

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            if(dx == 0)
            {
                for (int y = top; y <= bottom; y++)
                {
                    points.Add(new Point(left, y));
                }
            }
            else if(dy ==0)
            {
                for (int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }
            cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return cache.Values.SelectMany(points => points).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class Canvas
    {
        private static readonly List<VectorObject> vectorObjects 
            = new List<VectorObject>()
            {
                new VectorRectangle(1, 1, 10, 10),
                new VectorRectangle(3, 3, 6, 6)
            };

        public static void DrawPoint(Point p)
        {
            Console.Write(".");
        }

        public static void Generate()
        {
            foreach(VectorObject vO in vectorObjects)
            {
                foreach(Line line in vO)
                {
                    LineToPointAdapter adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                    Console.WriteLine("");
                }
            }
        }
    }
}
