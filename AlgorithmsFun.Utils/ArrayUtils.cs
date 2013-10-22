using System;

namespace AlgorithmsFun.Utils
{
    public static class ArrayUtils
    {
        public static void SplitArrayInHalves<TElement>(this TElement[] unsorted, out TElement[] half1, out TElement[] half2)
        {
            int halfSize = unsorted.Length / 2;
            half1 = new TElement[halfSize];
            Array.Copy(unsorted, half1, halfSize);
            int secondPartLength = unsorted.Length - halfSize;
            half2 = new TElement[secondPartLength];
            Array.Copy(unsorted, halfSize, half2, 0, secondPartLength);
        }
    }
}