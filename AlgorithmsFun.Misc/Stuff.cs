using AlgorithmsFun.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    /// <summary>
    /// All kinds of trash
    /// </summary>
    public static class Stuff
    {
        /// <summary>
        /// Determines whether a given integer is in the array
        /// </summary>
        /// <param name="array">sorted array of distinct values</param>
        /// <param name="number"></param>
        /// <returns>true if given number is in the array</returns>
        public static bool BinarySearchAdditionSubstraction(int[] array, int number)
        {
            if (array == null) throw new ArgumentNullException("array");
            
            if (array.Length == 0)
                return false;

            int fib = 0, prevFib = 0;
            foreach (var currentFib in GenerateFibonacciSequence())
            {
                if (currentFib >= array.Length)
                    break;
                prevFib = fib;
                fib = currentFib;
            }
            int start = 0;
            int end = array.Length - 1;
            while (end - start >= 0)
            {
                if (array[start + fib] == number)
                    return true;
                if (array[start + fib] > number)
                    end = start + fib - 1;
                else
                    start = start + fib + 1;
                ReduceFibonacci(ref fib, ref prevFib, end - start);
            }
            return false;
        }

        private static void ReduceFibonacci(ref int currentFibonacci, ref int prevFibonacci, int upperBound)
        {
            while (currentFibonacci >= upperBound)
            {
                int prevPrevFibonacci = currentFibonacci - prevFibonacci;
                currentFibonacci = prevFibonacci;
                prevFibonacci = prevPrevFibonacci;
            }   
        }

        private static IEnumerable<int> GenerateFibonacciSequence()
        {
            int prevprev = 0;
            yield return prevprev;
            int prev = 1;
            yield return prev;
            while (true)
            {
                int current = prev + prevprev;
                prevprev = prev;
                prev = current;
                yield return current;
            }
        }

        /// <summary>
        /// Determines wether a given integer is in the array
        /// </summary>
        /// <param name="array">bitonic array of distinct numbers</param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool BitonicSearch(int[] array, int num)
        {
            if (array == null) throw new ArgumentNullException("array");

            if (array.Length == 0) return false;

            int midPoint = FindMidPoint(array);
            int leftPosition = array.SearchBinary(0, midPoint, (middle, left, right) =>
                {
                    if (middle == num)
                        return 0;
                    if (middle < num)
                        return 1;
                    return -1;
                });
            if (leftPosition >= 0)
                return true;
            int rightPosition = array.SearchBinary(midPoint, array.Length - 1, (middle, left, right) =>
                {
                    if (middle == num)
                        return 0;
                    if (middle < num)
                        return -1;
                    return 1;
                });
            return rightPosition >= 0;
        }

        private static int FindMidPoint(int[] array)
        {
            var result = array.SearchBinary((middle, left, right) =>
                {
                    if ((!left.HasValue && !right.HasValue) ||
                        ((!left.HasValue || middle > left.Value) && (!right.HasValue || middle > right.Value)))
                        return 0;
                    if (right.HasValue)
                    {
                        if (middle < right.Value)
                            return 1;
                        return -1;
                    }
                    if (middle < left.Value)
                        return -1;
                    return 1;
                });
            return result;
        }

        /// <summary>
        ///     Finds local minimum (pair of indeces i, j) : a[i, j] < a[ i+1, j], a[ i, j] < a[ i, j+1], a[ i, j] < a[ i-1, j], a[
        ///         i, j] < a[ i, j-1]
        /// </summary>
        /// <param name="a">array of distinct integers</param>
        /// <returns>local minimum indeces</returns>
        public static Tuple<int, int> FindMatrixLocalMinimum(int[,] a)
        {
            if (a == null) throw new ArgumentNullException("a");
            if (a.Length == 0) throw new ArgumentException();

            var result = GetMatrixMinimumRec(a, 0, a.GetLength(0), 0, a.GetLength(0));
            return result;
        }

        private static Tuple<int, int> GetMatrixMinimumRec(int[,] a, int startX, int endX, int startY, int endY)
        {
            if (startX == endX && startY == endY) return new Tuple<int, int>(startX, startY);

            int middleX = startX + (endX - startX)/2;
            int middleY = startY + (endY - startY)/2;

            int min = a[middleX, startY];
            int minX = middleX;
            int minY = startY;

            for (int i = startY; i <= endY; i++)
            {
                if (a[middleX, i] < min)
                {
                    minY = i;
                    min = a[middleX, i];
                }
            }

            for (int i = startX; i < endX; i++)
            {
                if (a[i, middleY] < min)
                {
                    minX = i;
                    minY = middleY;
                    min = a[i, middleY];
                }
            }
            var nextField = GetNextField(a, startX, startY, endX, endY, minX, minY);
            if (nextField.Item1.Item1 == startX && nextField.Item1.Item2 == endX && nextField.Item2.Item1 == startY &&
                nextField.Item2.Item2 == endY)
                return new Tuple<int, int>(minX, minY);
            return GetMatrixMinimumRec(a, nextField.Item1.Item1, nextField.Item1.Item2, nextField.Item2.Item1,
                                       nextField.Item2.Item2);
        }

        private static Tuple<Tuple<int, int>, Tuple<int, int>> GetNextField(int[,] a, int startX, int startY, int endX,
                                                                            int endY, int minX, int minY)
        {
            var cells = new List<Quadrant>(5) {new Quadrant(Direction.Stay, a[minX, minY])};

            if (minX > startX)
                cells.Add(new Quadrant(Direction.Left, a[minX - 1, minY]));
            if (minX < endX)
                cells.Add(new Quadrant(Direction.Right, a[minX + 1, minY]));
            if (minY > startY)
                cells.Add(new Quadrant(Direction.Top, a[minX, minY - 1]));
            if (minY < endY)
                cells.Add(new Quadrant(Direction.Bottom, a[minX, minY + 1]));
            Quadrant minQuadrant = cells.Min();
            switch (minQuadrant.Direction)
            {
                case Direction.Stay:
                    return new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(startX, endX),
                                                                       new Tuple<int, int>(startY, endY));
                case Direction.Left:
                    return new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(startX, minX - 1),
                                                                       new Tuple<int, int>(startY, endY));
                case Direction.Right:
                    return new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(minX + 1, endX),
                                                                       new Tuple<int, int>(startY, endY));
                case Direction.Top:
                    return new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(startX, endX),
                                                                       new Tuple<int, int>(startY, minY - 1));
                case Direction.Bottom:
                    return new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(startX, endX),
                                                                       new Tuple<int, int>(minY + 1, endY));
                default:
                    throw new NotSupportedException();
            }
        }


        /// <summary>
        ///     Finds local minimum (index i): a[i-1] > a[i] < a[ i+1]
        /// </summary>
        /// <param name="a">array of distinct integers with 1 local minimum</param>
        /// <returns>index i</returns>
        public static int FindArrayLocalMinimum(int[] a)
        {
            if (a == null) throw new ArgumentNullException("a");
            if (a.Length == 0) throw new ArgumentException();

            int result = GetMinimumRec(a, 0, a.Length - 1);
            return result;
        }

        private static int GetMinimumRec(int[] a, int start, int end)
        {
            if (start == end) return start;

            int middle = start + (end - start)/2;
            if (a[middle] < a[middle + 1])
            {
                if (middle - 1 < start || a[middle] < a[middle - 1])
                {
                    return middle;
                }
                return GetMinimumRec(a, start, middle - 1);
            }
            return GetMinimumRec(a, middle + 1, end);
        }

        private enum Direction
        {
            Left,
            Right,
            Top,
            Bottom,
            Stay
        }

        private sealed class Quadrant : IComparable<Quadrant>
        {
            public readonly Direction Direction;
            private readonly int _value;

            public Quadrant(Direction direction, int value)
            {
                Direction = direction;
                _value = value;
            }

            public int CompareTo(Quadrant other)
            {
                return _value.CompareTo(other._value);
            }
        }
    }
}