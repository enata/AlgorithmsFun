using AlgorithmsFun.DivideConquer;
using NUnit.Framework;

namespace AlgorithmsFun.Tests.DivideConquer
{
    [TestFixture]
    public sealed class PointUtilsTests
    {
        [Test]
        public void ClosestPairTest()
        {
            var points = new[] {new Point2D(3, 2), new Point2D(7, 3), new Point2D(4, 1), new Point2D(0, 0)};
            PointPair closestPair = points.ClosestPair();
            Assert.IsTrue((closestPair.Point1.X == 3.0 && closestPair.Point1.Y == 2.0) ||
                          (closestPair.Point1.X == 4.0 && closestPair.Point1.Y == 1.0));
            Assert.IsTrue((closestPair.Point2.X == 3.0 && closestPair.Point2.Y == 2.0) ||
                          (closestPair.Point2.X == 4.0 && closestPair.Point2.Y == 1.0));
        }
    }
}