using System.Collections.Generic;
using AlgorithmsFun.Utils;

namespace AlgorithmsFun.Sorting
{
    public sealed class MergeSorter<TSortable> : ISorter<TSortable>
    {
        private readonly IComparer<TSortable> _comparer;
 
        public MergeSorter(IComparer<TSortable> comparer)
        {
            _comparer = comparer;
        }

        public MergeSorter() : this(Comparer<TSortable>.Default)
        {}

        public void Sort(ref TSortable[] unsorted)
        {
            if (unsorted.Length <= 1)
                return;

            TSortable[] half1, half2;
            unsorted.SplitArrayInHalves(out half1, out half2);

            // recursively sort 1st part of the input array
            Sort(ref half1);
            // recursively sort 2nd part of the input array
            Sort(ref half2);
            // merge two sorted sublists into one
            unsorted = Merge(half1, half2);
        }


        private TSortable[] Merge(TSortable[] sorted1, TSortable[] sorted2)
        {
            var result = new TSortable[sorted1.Length + sorted2.Length];
            int positionIn1 = 0;
            int positionIn2 = 0;

            for (int i = 0; i < result.Length; i++)
            {
                if (positionIn2 > sorted2.Length - 1 ||
                    (positionIn1 < sorted1.Length && _comparer.Compare(sorted1[positionIn1], sorted2[positionIn2]) < 0))
                {
                    result[i] = sorted1[positionIn1];
                    positionIn1++;
                }
                else
                {
                    result[i] = sorted2[positionIn2];
                    positionIn2++;
                }
            }
            return result;
        }
    }
}