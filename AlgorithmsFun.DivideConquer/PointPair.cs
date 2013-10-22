namespace AlgorithmsFun.DivideConquer
{
    public struct PointPair
    {
        private readonly Point2D _point1;
        private readonly Point2D _point2;
        private readonly double _distance;

        public PointPair(Point2D point1, Point2D point2)
        {
            _point1 = point1;
            _point2 = point2;
            _distance = point1.EuclidianDistance(point2);
        }

        public Point2D Point1
        {
            get { return _point1; }
        }

        public Point2D Point2
        {
            get { return _point2; }
        }

        public double Distance
        {
            get { return _distance; }
        }
    }
}