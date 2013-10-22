using System;

namespace AlgorithmsFun.Utils
{
    public static class MathUtils
    {
        public static bool IsAbout(this double number, double sample, double accuracy = double.Epsilon)
        {
            return Math.Abs(number - sample) < accuracy;
        }
    }
}