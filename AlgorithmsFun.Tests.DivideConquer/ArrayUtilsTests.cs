using AlgorithmsFun.DivideConquer;
using NUnit.Framework;

namespace AlgorithmsFun.Tests.DivideConquer
{
    [TestFixture]
    public sealed class ArrayUtilsTests
    {
        [Test]
        public void InversionsTest()
        {
            var array = new[] {1, 3, 5, 2, 4, 6};
            int inversions = array.Inversions();
            Assert.AreEqual(3, inversions);
        }
    }
}