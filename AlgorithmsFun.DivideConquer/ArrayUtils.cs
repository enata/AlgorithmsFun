using System;
using System.Collections.Generic;
using AlgorithmsFun.Utils;

namespace AlgorithmsFun.DivideConquer
{
    public static class ArrayUtils
    {
        public static int Inversions<TElement>(this TElement[] array, IComparer<TElement> comparer)
        {
            var localArray = new TElement[array.Length];
            Array.Copy(array, localArray, array.Length);
            return SortCalculateInversions(ref localArray, comparer);
        }

        public static int Inversions<TElement>(this TElement[] array)
        {
            return Inversions(array, Comparer<TElement>.Default);
        }


        private static int SortCalculateInversions<TElement>(ref TElement[] unsorted, IComparer<TElement> comparer)
        {
            if (unsorted.Length <= 1)
                return 0;

            TElement[] half1, half2;
            unsorted.SplitArrayInHalves(out half1, out half2);

            int inversionsIn1 = SortCalculateInversions(ref half1, comparer);
            int inversionsIn2 = SortCalculateInversions(ref half2, comparer);
            int splitInversions;
            unsorted = MergeCount(half1, half2, comparer, out  splitInversions);
            return inversionsIn1 + inversionsIn2 + splitInversions;
        }     

        private static TElement[] MergeCount<TElement>(TElement[] sorted1, TElement[] sorted2, IComparer<TElement> comparer, out int inversions)
        {
            var result = new TElement[sorted1.Length + sorted2.Length];
            int positionIn1 = 0;
            int positionIn2 = 0;

            // while merging the two sorted subarrays, keep running total of number of split inversions
            inversions = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (positionIn2 > sorted2.Length - 1 ||
                    (positionIn1 < sorted1.Length && comparer.Compare(sorted1[positionIn1], sorted2[positionIn2]) < 0))
                {
                    result[i] = sorted1[positionIn1];
                    positionIn1++;
                }
                else
                {
                    // when element of 2nd array gets copied to output, increment total by number of elements remaining in 1st array 
                    if (positionIn1 < sorted1.Length)
                    {
                        inversions += sorted1.Length - positionIn1;
                    }
                    result[i] = sorted2[positionIn2];
                    positionIn2++;
                }
            }
            return result;
        }
    }
}