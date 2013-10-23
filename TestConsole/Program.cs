using System;
using System.Diagnostics;
using AlgorithmsFun.Matrices;

namespace TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var m1 = GenerateRandomMatrix(200, 200);
            var m2 = GenerateRandomMatrix(200, 200);

            const int iterations = 20;

            var timer = new Stopwatch();
            timer.Start();
            
            Console.WriteLine("Classic multiplication started: ");
            for (int i = 0; i < iterations; i++)
            {
                Matrix.MultiplyClassic(m1, m2);
                Console.WriteLine("Classic {0}", i);
            }   

            timer.Stop();
            Debug.WriteLine("Classic multiplication time: {0}", timer.Elapsed);
            timer.Reset();
            timer.Start();
            Console.WriteLine("Enhanced multiplication started: ");
            for (int i = 0; i < iterations; i++)
            {
                 Matrix.MultiplyStrassen(m1,m2);
                Console.WriteLine("Enhanced {0}", i);
            }
            timer.Stop();
            Debug.WriteLine("Enhanced multiplication time: {0}", timer.Elapsed);
        }

        private static Matrix GenerateRandomMatrix(int rows, int columns)
        {
            var rnd = new Random();
            var result = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = rnd.Next();
                }
            }

            return result;
        }
    }
}