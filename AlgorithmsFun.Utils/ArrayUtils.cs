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

        public static int SearchBinary<TElement>(this TElement[] sorted, Func<TElement, TElement?, TElement?, int> stopCondition) where TElement:struct 
        {
            if (sorted == null) throw new ArgumentNullException("sorted");

            if (sorted.Length == 0)
                return -1;
            return SearchBinary(sorted, 0, sorted.Length - 1, stopCondition);
        }

        public static int SearchBinary<TElement>(this TElement[] sorted, int start, int end,
                                                  Func<TElement, TElement?, TElement?, int> stopCondition) where TElement:struct 
        {
            if (sorted == null) throw new ArgumentNullException("sorted");

            if(start > end) return -1;

            if (sorted.Length == 0)
                return -1;

            if (start == end)
                return stopCondition.Invoke(sorted[start], null, null) == 0 ? start : -1;

            int midPoint = start + (end - start)/2;
            TElement? left = midPoint > start ? (TElement?) sorted[midPoint - 1] : null;
            TElement? right = midPoint < end ? (TElement?) sorted[midPoint + 1] : null;
            var searchPlace = stopCondition.Invoke(sorted[midPoint], left, right);
            if (searchPlace > 0)
                return SearchBinary(sorted, midPoint + 1, end, stopCondition);
            if(searchPlace < 0)
                return SearchBinary(sorted, start, midPoint - 1, stopCondition);
            return midPoint;
        }
    }
}