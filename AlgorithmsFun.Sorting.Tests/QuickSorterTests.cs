using NUnit.Framework;

namespace AlgorithmsFun.Sorting.Tests
{
    [TestFixture]
    public sealed class QuickSorterTests : SorterTests
    {
        private readonly QuickSorter<int> _sorter = new QuickSorter<int>(); 

        protected override ISorter<int> Sorter
        {
            get { return _sorter; }
        }
    }
}