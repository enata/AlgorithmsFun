using System;
using System.Collections.Generic;

namespace AlgorithmsFun.Sorting
{
    public sealed class QuickSorter<TSortable> : ISorter<TSortable>
    {
        private readonly IComparer<TSortable> _comparer;
        private readonly Random _randomizer;

        public QuickSorter(IComparer<TSortable> comparer)
        {
            _comparer = comparer;
            _randomizer = new Random();
        }

        public QuickSorter() : this(Comparer<TSortable>.Default) 
        {}

        public void Sort(ref TSortable[] unsorted)
        {
            if(unsorted.Length <= 1)
                return;

            Sort(unsorted, 0, unsorted.Length - 1);
        }

        private void Sort(TSortable[] unsorted, int left, int right)
        {
            if (right - left <= 0)
                return;

            var newPivot = ChoosePivot(left, right);
            Swap(unsorted, newPivot, left);

            // Partition array around pivot
            var pivot = Partition(unsorted, left, right);

            // Recursively sort 1st part
            Sort(unsorted, left, pivot - left);

            // Recursively sort 2nd part
            Sort(unsorted, pivot + 1, right);
        }

        private int Partition(TSortable[] unsorted, int left, int right)
        {
            // Pick element of array
            TSortable pivot = unsorted[left];

            // Rearrange array so that
            // LeE of pivot => less than pivot
            // Right of pivot => greater than pivot
            int lessBorder = left + 1;
            for (int greaterBorder = left + 1; greaterBorder <= right; greaterBorder++)
            {
                if (_comparer.Compare(unsorted[greaterBorder], pivot) < 0)
                {
                    Swap(unsorted, lessBorder, greaterBorder);
                    lessBorder++;
                }
            }
            int result = lessBorder - 1;
            Swap(unsorted, left, result);
            return result;
        }

        private void Swap(TSortable[] array, int position1, int position2)
        {
            if(position1 == position2)
                return;
            
            var temp = array[position1];
            array[position1] = array[position2];
            array[position2] = temp;
        }

        private int ChoosePivot(int left, int right)
        {
            int result = _randomizer.Next(left, right + 1);
            return result;
        }
    }
}