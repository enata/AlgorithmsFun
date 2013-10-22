using System;
using AlgorithmsFun.Matrices;
using AlgorithmsFun.Utils;
using NUnit.Framework;

namespace AlgorithmsFun.Tests.Matrices
{
    [TestFixture]
    public sealed class MatrixTest
    {
        [Test]
        public void ComplexOperationSet()
        {
            var a = new Matrix(new[,] {{1.0}, {4.0}, {2.0}});
            var b = new Matrix(new[,] {{0.0}, {0.0}, {5.0}});
            var c = new Matrix(new[,] {{3.0}, {0.0}, {2.0}});

            var result = 3.0*a + b - c/3.0;
            
            Assert.AreEqual(3, result.Rows);
            Assert.AreEqual(1, result.Columns);
            Assert.AreEqual(2.0, result[0,0]);
            Assert.AreEqual(12.0, result[1,0]);
            Assert.IsTrue(result[2,0].IsAbout(10.33, 0.01));
        }

        [Test]
        public void ScalarDivisionTest()
        {
            var matrix = new Matrix(new[,] {{4.0, 0.0}, {6.0, 3.0}});
            
            var result = matrix/4.0;

            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Columns);
            Assert.AreEqual(1.0, result[0,0]);
            Assert.AreEqual(0.0, result[0,1]);
            Assert.AreEqual(1.5, result[1,0]);
            Assert.AreEqual(0.75, result[1,1]);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ScalarDivisionMatrixNullExc()
        {
            Matrix matrix = null;
            var result = matrix/3.0;
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void ScalarDivisionByZeroExc()
        {
            var matrix = new Matrix(new[,] {{1.0}});
            var result = matrix/0.0;
        }

        [Test]
        public void MinusTest()
        {
            var a = new Matrix(new[,] {{-2.0, 1.0}, {5.0, 4.0}});
            var b = new Matrix(new[,] {{3.0, 1.0}, {-1.0, 0.0}});

            Matrix result = a - b;

            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Columns);
            Assert.AreEqual(-5.0, result[0, 0]);
            Assert.AreEqual(6.0, result[1, 0]);
            Assert.AreEqual(0.0, result[0, 1]);
            Assert.AreEqual(4.0, result[1, 1]);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void MinusTestInconsistentDimensionsErr()
        {
            var a = new Matrix(new[,] {{-2.0, 1.0}, {5.0, 4.0}});
            var b = new Matrix(new[,] {{3.0}, {-1.0}});

            Matrix result = a - b;
        }

        [Test]
        public void MultiplicationMatrixScalarTest()
        {
            var matrix = new Matrix(new[,] {{1.0, 0.0}, {2.0, 5.0}, {3.0, 1.0}});

            Matrix result = matrix*3.0;


            Assert.AreEqual(3, result.Rows);
            Assert.AreEqual(2, result.Columns);
            Assert.AreEqual(3.0, result[0, 0]);
            Assert.AreEqual(0.0, result[0, 1]);
            Assert.AreEqual(6.0, result[1, 0]);
            Assert.AreEqual(15.0, result[1, 1]);
            Assert.AreEqual(9.0, result[2, 0]);
            Assert.AreEqual(3.0, result[2, 1]);
        }

        [Test]
        public void MultiplicationScalarMatrixTest()
        {
            var matrix = new Matrix(new[,] {{1.0, 0.0}, {2.0, 5.0}, {3.0, 1.0}});

            Matrix result = 3.0*matrix;

            Assert.AreEqual(3, result.Rows);
            Assert.AreEqual(2, result.Columns);
            Assert.AreEqual(3.0, result[0, 0]);
            Assert.AreEqual(0.0, result[0, 1]);
            Assert.AreEqual(6.0, result[1, 0]);
            Assert.AreEqual(15.0, result[1, 1]);
            Assert.AreEqual(9.0, result[2, 0]);
            Assert.AreEqual(3.0, result[2, 1]);
        }

        [Test]
        public void MultiplicationTest()
        {
            var a = new Matrix(new[,] {{-2.0, 1.0}, {5.0, 4.0}});
            var b = new Matrix(new[,] {{3.0}, {-1.0}});

            Matrix result = a*b;

            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(1, result.Columns);
            Assert.AreEqual(-7.0, result[0, 0]);
            Assert.AreEqual(11.0, result[1, 0]);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void MultiplicationTestInconsistentDimensionsErr()
        {
            var a = new Matrix(1, 3);
            var b = new Matrix(1, 2);
            Matrix r = a*b;
        }

        [Test]
        public void MultiplicationTestLarger()
        {
            var a = new Matrix(new[,] {{5.0, 8.0, -4.0}, {6.0, 9.0, -5.0}, {4.0, 7.0, -3.0}});
            var b = new Matrix(new[,] {{3.0, 2.0, 5.0}, {4.0, -1.0, 3.0}, {9.0, 6.0, 5.0}});

            Matrix result = a*b;
            Assert.AreEqual(3, result.Rows);
            Assert.AreEqual(3, result.Columns);
            Assert.AreEqual(11.0, result[0, 0]);
            Assert.AreEqual(-22.0, result[0, 1]);
            Assert.AreEqual(29.0, result[0, 2]);
            Assert.AreEqual(9.0, result[1, 0]);
            Assert.AreEqual(-27.0, result[1, 1]);
            Assert.AreEqual(32.0, result[1, 2]);
            Assert.AreEqual(13.0, result[2, 0]);
            Assert.AreEqual(-17.0, result[2, 1]);
            Assert.AreEqual(26.0, result[2, 2]);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SumTest1MatrixNullExc()
        {
            var b = new Matrix(new[,] {{1.0}});
            Matrix result = null*b;
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SumTest2MatrixNullExc()
        {
            var a = new Matrix(new[,] {{1.0}});
            Matrix result = a*null;
        }

        [Test]
        public void SumTest3X2()
        {
            var a = new Matrix(new[,] {{1.0, 0.0}, {2.0, 5.0}, {3.0, 1.0}});
            var b = new Matrix(new[,] {{4.0, 0.5}, {2.0, 5.0}, {0.0, 1.0}});

            Matrix result = a + b;
            Assert.AreEqual(3, result.Rows);
            Assert.AreEqual(2, result.Columns);
            Assert.AreEqual(5.0, result[0, 0]);
            Assert.AreEqual(0.5, result[0, 1]);
            Assert.AreEqual(4.0, result[1, 0]);
            Assert.AreEqual(10.0, result[1, 1]);
            Assert.AreEqual(3.0, result[2, 0]);
            Assert.AreEqual(2.0, result[2, 1]);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void SumTestInconsistentDimensionsErr()
        {
            var a = new Matrix(new[,] {{1.0, 0.0}, {2.0, 5.0}, {3.0, 1.0}});
            var b = new Matrix(new[,] {{4.0, 0.5}, {2.0, 5.0}});

            Matrix result = a + b;
        }
    }
}