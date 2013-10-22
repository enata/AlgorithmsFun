using System;

namespace AlgorithmsFun.DivideConquer
{
    public struct Point2D
    {
        private readonly double _x;
        private readonly double _y;


        public Point2D(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
        }

        public double EuclidianDistance(Point2D point)
        {
            double result = Math.Sqrt(Math.Pow(X - point.X, 2.0) + Math.Pow(Y - point.Y, 2.0));
            return result;
        }
    }
}