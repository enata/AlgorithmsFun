using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    public static class Stuff
    {
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
            public readonly int Value;

            public Quadrant(Direction direction, int value)
            {
                Direction = direction;
                Value = value;
            }

            public int CompareTo(Quadrant other)
            {
                return Value.CompareTo(other.Value);
            }
        }
    }
}