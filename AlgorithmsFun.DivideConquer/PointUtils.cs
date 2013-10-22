using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsFun.Utils;

namespace AlgorithmsFun.DivideConquer
{
    public static class PointUtils
    {
        public static PointPair ClosestPair(this IEnumerable<Point2D> points)
        {
            Point2D[] enumeratedPoints = points.ToArray();
            if (enumeratedPoints.Length <= 1)
                throw new InvalidOperationException("Not enough points in the collection");

            Point2D[] xArray = enumeratedPoints.OrderBy(point => point.X)
                                               .ToArray();
            Point2D[] yArray = enumeratedPoints.OrderBy(point => point.Y)
                                               .ToArray();

            return ClosestPair(xArray, yArray);
        }

        private static PointPair ClosestPair(Point2D[] xSorted, Point2D[] ySorted)
        {
            if (xSorted.Length == 2)
                return new PointPair(xSorted[0], xSorted[1]);

            Point2D[] leftXSorted, rightXSorted;
            Point2D[] leftYSorted, rightYSorted;
            xSorted.SplitArrayInHalves(out leftXSorted, out rightXSorted);
            ySorted.SplitArrayInHalves(out leftYSorted, out rightYSorted);

            PointPair closestLeftPair = ClosestPair(leftXSorted, leftYSorted);
            PointPair closestRightPair = ClosestPair(rightXSorted, rightYSorted);
            double currentMinimum = Math.Min(closestLeftPair.Distance, closestRightPair.Distance);
            PointPair? closestSplit = ClosestSplitPair(xSorted, ySorted, currentMinimum);

            return GetClosestPair(closestLeftPair, closestRightPair, closestSplit);
        }

        private static PointPair GetClosestPair(PointPair closestLeftPair, PointPair closestRightPair,
                                                PointPair? closestSplit)
        {
            if (closestLeftPair.Distance < closestRightPair.Distance)
            {
                if (!closestSplit.HasValue || closestLeftPair.Distance < closestSplit.Value.Distance)
                    return closestLeftPair;
                return closestSplit.Value;
            }
            if (!closestSplit.HasValue || closestRightPair.Distance < closestSplit.Value.Distance)
                return closestRightPair;
            return closestSplit.Value;
        }

        private static PointPair? ClosestSplitPair(Point2D[] xSorted, Point2D[] ySorted, double currentMinimum)
        {
            int center = xSorted.Length/2;
            double centerX = xSorted[center - 1].X;
            double leftBorder = centerX - currentMinimum;
            double rightBorder = centerX + currentMinimum;
            if (leftBorder > rightBorder)
            {
                double tmp = leftBorder;
                leftBorder = rightBorder;
                rightBorder = tmp;
            }

            Point2D[] closePairs = ySorted.Where(point => point.X >= leftBorder && point.X <= rightBorder)
                                          .ToArray();
            double best = currentMinimum;
            PointPair? closestPair = null;
            for (int i = 0; i < closePairs.Length; i++)
            {
                int lookupBorder = Math.Min(i + 7, closePairs.Length - 1);
                for (int j = i + 1; j <= lookupBorder; j++)
                {
                    var pair = new PointPair(closePairs[i], closePairs[j]);
                    if (pair.Distance < best)
                    {
                        best = pair.Distance;
                        closestPair = pair;
                    }
                }
            }

            return closestPair;
        }
    }
}