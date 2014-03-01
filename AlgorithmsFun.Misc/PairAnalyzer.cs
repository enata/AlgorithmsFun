using System;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    public sealed class PairAnalyzer
    {
        private static Tuple<double, double> GetPair(double[] a, Func<double[], Tuple<double, double>> sortedArrayAnalyzer)
        {
            if(a == null) throw new ArgumentNullException("a");
            if(a.Length < 2) throw new ArgumentException();

            var sorted = a.OrderBy(el => el).ToArray();
            var result = sortedArrayAnalyzer.Invoke(sorted);
            return result;
        }

        private static Tuple<double, double> PairDistanceCompare(double[] a, Func<double, double, bool> distanceComparer)
        {
            var result = GetPair(a, doubles =>
                {
                    var bestDist = doubles[1] - doubles[0];
                    var bestPair = new Tuple<double, double>(doubles[0], doubles[1]);
                    for (int i = 2; i < a.Length; i++)
                    {
                        var dist = doubles[i] - doubles[i - 1];
                        if (distanceComparer.Invoke(dist, bestDist))
                        {
                            bestDist = dist;
                            bestPair = new Tuple<double, double>(doubles[i - 1], doubles[i]);
                        }
                    }
                    return bestPair;
                });
            return result;
        }

        public Tuple<double,double> GetClosestPair(double[] a)
        {
            var result = PairDistanceCompare(a, (d, bestD) => d < bestD);
            return result;
        }

        public Tuple<double, double> GetFarthestPair(double[] a)
        {
            if(a == null) throw new ArgumentNullException();
            if(a.Length < 2) throw new ArgumentException();

            double min = Math.Min(a[0], a[1]);
            double max = Math.Max(a[0], a[1]);
            for (int i = 2; i < a.Length; i++)
            {
                if (a[i] < min)
                    min = a[i];
                else if (a[i] > max)
                    max = a[i];
            }
            return new Tuple<double, double>(min, max);
        }
    }
}