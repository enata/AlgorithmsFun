using NUnit.Framework;

namespace AlgorithmsFun.Sorting.Tests
{
    [TestFixture]
    public sealed class MergeSorterTests : SorterTests
    {
        private readonly MergeSorter<int> _sorter = new MergeSorter<int>(); 

        protected override ISorter<int> Sorter
        {
            get { return _sorter; }
        }
    }
}