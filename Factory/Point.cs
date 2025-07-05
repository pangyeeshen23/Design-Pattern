using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Factory
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    // Simple Factory Pattern
    //public class Point
    //{
    //    private double x, y;

    //    private Point(double x, double y)
    //    {
    //        this.x = x;
    //        this.y = y;
    //    }

    //    public override string ToString()
    //    {
    //        return $"{nameof(x)}: {x}, {nameof(y)} : {y}";
    //    }

    //    public static Point NewCartesianPoint(double x, double y)
    //    {
    //        return new Point(x, y);
    //    }
    //    public static Point NewPolarPoint(double rho, double theta)
    //    {
    //        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    //    }
    //}

    // Dedicated Point Factory For Point
    //public static class PointFactory
    //{
    //    public static Point NewCartesianPoint(double x, double y)
    //    {
    //        return new Point(x, y);
    //    }
    //    public static Point NewPolarPoint(double rho, double theta)
    //    {
    //        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    //    }
    //}

    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point Origin => new Point(0, 0);
        public static Point Origin2 => new Point(0, 0);

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)} : {y}";
        }
        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }
            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }
}
