using System;

namespace AlgorithmsFun.Matrices
{
    public sealed class Matrix
    {
        private readonly double[,] _values;

        public Matrix(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentException("Rows and columns number should be > 0");

            _values = new double[rows,columns];
        }

        public Matrix(double[,] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            _values = array;
        }

        public int Rows
        {
            get { return _values.GetLength(0); }
        }

        public int Columns
        {
            get { return _values.GetLength(1); }
        }

        public double this[int i, int j]
        {
            get { return _values[i, j]; }
            set { _values[i, j] = value; }
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
                throw new ArgumentNullException();

            return CreateCombinedMatrix(matrix1, matrix2, (n1, n2) => n1 + n2);
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
                throw new ArgumentNullException();

            return CreateCombinedMatrix(matrix1, matrix2, (n1, n2) => n1 - n2);
        }

        /// <summary>
        /// Scalar multiplication
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="multiplier"></param>
        /// <returns>result of scalar matrix multiplication</returns>
        public static Matrix operator *(Matrix matrix, double multiplier)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix");
            }

            var newMatrixArray = new double[matrix.Rows,matrix.Columns];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    newMatrixArray[i, j] = matrix[i, j]*multiplier;
                }
            }
            var result = new Matrix(newMatrixArray);
            return result;
        }

        public static Matrix operator /(Matrix matrix, double divisor)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix");
            }
            if (divisor == 0.0)
            {
                throw new InvalidOperationException("Division by zero");
            }

            var multiplier = 1.0/divisor;
            var result = matrix*multiplier;
            return result;
        }

        public static Matrix operator *(double multiplier, Matrix matrix)
        {
            return matrix*multiplier;
        }

        //Strassen's matrix multiplication
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            if (a.Columns != b.Rows)
                throw new InvalidOperationException(
                    "Number of rows in the 1st array and columns in the 2nd should be equal");

            if (a.Rows == 0)
            {
                return new Matrix(0, 0);
            }

            int arraySize = Math.Max(Math.Max(a.Rows, b.Rows), a.Columns);
            if (arraySize%2 != 0)
                arraySize += 1;

            Matrix extendedA = ExtendMatrix(a, arraySize);
            Matrix extendedB = ExtendMatrix(b, arraySize);

            Matrix extendedResult = SquareMatrixMultiplication(extendedA, extendedB);

            if (extendedResult.Rows == a.Rows && extendedResult.Columns == b.Columns)
            {
                return extendedResult;
            }

            var result = new Matrix(a.Rows, b.Columns);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    result[i, j] = extendedResult[i, j];
                }
            }
            return result;
        }

        private static Matrix SquareMatrixMultiplication(Matrix a, Matrix b)
        {
            int dimensions = a.Rows;

            var result = new Matrix(dimensions, dimensions);
            if (dimensions == 1)
            {
                result[0, 0] = a[0, 0]*b[0, 0];
                return result;
            }

            Matrix a11, a21, a12, a22;
            Matrix b11, b21, b12, b22;

            a.BreakSquareMatrix(out a11, out a21, out a12, out a22);
            b.BreakSquareMatrix(out b11, out b21, out b12, out b22);

            //recursively compute 7 products
            Matrix m1 = SquareMatrixMultiplication(a11 + a22, b11 + b22);
            Matrix m2 = SquareMatrixMultiplication(a21 + a22, b11);
            Matrix m3 = SquareMatrixMultiplication(a11, b12 - b22);
            Matrix m4 = SquareMatrixMultiplication(a22, b21 - b11);
            Matrix m5 = SquareMatrixMultiplication(a11 + a12, b22);
            Matrix m6 = SquareMatrixMultiplication(a21 - a11, b11 + b12);
            Matrix m7 = SquareMatrixMultiplication(a12 - a22, b21 + b22);

            //do the necessary additions + substractions
            Matrix result11 = m1 + m4 - m5 + m7;
            Matrix result12 = m3 + m5;
            Matrix result21 = m2 + m4;
            Matrix result22 = m1 - m2 + m3 + m6;

            int halfResult = result11.Rows;
            result11.CopyTo(result, 0, 0);
            result12.CopyTo(result, 0, halfResult);
            result21.CopyTo(result, halfResult, 0);
            result22.CopyTo(result, halfResult, halfResult);
            return result;
        }

        private void CopyTo(Matrix target, int targetRowsStartIndex, int targetColumnsStartIndex)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    target[i + targetRowsStartIndex, j + targetColumnsStartIndex] = _values[i, j];
                }
            }
        }

        private void BreakSquareMatrix(out Matrix aSubmatrix1, out Matrix aSubmatrix2, out Matrix aSubmatrix3,
                                       out Matrix aSubmatrix4)
        {
            int submatrixSize = Rows/2;
            aSubmatrix1 = GetSubmatrix(0, submatrixSize, 0, submatrixSize);
            aSubmatrix2 = GetSubmatrix(submatrixSize, submatrixSize, 0, submatrixSize);
            aSubmatrix3 = GetSubmatrix(0, submatrixSize, submatrixSize, submatrixSize);
            aSubmatrix4 = GetSubmatrix(submatrixSize, submatrixSize, submatrixSize, submatrixSize);
        }

        private Matrix GetSubmatrix(int rowsStartIndex, int rowsNumber, int columnsStartIndex, int columnsNumber)
        {
            var result = new Matrix(rowsNumber, columnsNumber);
            for (int i = 0; i < rowsNumber; i++)
            {
                for (int j = 0; j < columnsNumber; j++)
                {
                    result[i, j] = _values[rowsStartIndex + i, columnsStartIndex + j];
                }
            }
            return result;
        }

        private static Matrix ExtendMatrix(Matrix initial, int extendedSize)
        {
            if (initial.Rows == extendedSize && initial.Columns == extendedSize)
                return initial;
            var result = new Matrix(extendedSize, extendedSize);
            for (int i = 0; i < extendedSize; i++)
            {
                for (int j = 0; j < extendedSize; j++)
                {
                    bool isAdditionalCell = i >= initial.Rows || j >= initial.Columns;
                    result[i, j] = isAdditionalCell ? 0.0 : initial[i, j];
                }
            }
            return result;
        }

        private static Matrix CreateCombinedMatrix(Matrix matrix1, Matrix matrix2,
                                                   Func<double, double, double> combination)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new InvalidOperationException("Matrices are of different sizes");

            var result = new Matrix(matrix1.Rows, matrix1.Columns);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    result[i, j] = combination.Invoke(matrix1[i, j], matrix2[i, j]);
                }
            }
            return result;
        }
    }
}