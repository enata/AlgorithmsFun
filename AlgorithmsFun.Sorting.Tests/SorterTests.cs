using NUnit.Framework;

namespace AlgorithmsFun.Sorting.Tests
{
    public abstract class SorterTests
    {
        protected abstract ISorter<int> Sorter { get; }

        [Test]
        public void SortTest()
        {
            var unsorted = new[] {5, 4, 1, 8, 7, 2, 6, 3};
            var sorted = new[] {1, 2, 3, 4, 5, 6, 7, 8};
            TestSort(unsorted, sorted);
        }

        [Test]
        public void Sort3Test()
        {
            var unsorted = new[] {5, 4, 1};
            var sorted = new[] {1, 4, 5};
            TestSort(unsorted, sorted);
        }

        private void TestSort(int[] unsorted, int[] sorted)
        {
            int length = unsorted.Length;
            Sorter.Sort(ref unsorted);
            Assert.AreEqual(length, unsorted.Length);
            for (int i = 0; i < unsorted.Length; i++)
            {
                Assert.AreEqual(sorted[i], unsorted[i]);
            }
        }
    }
}