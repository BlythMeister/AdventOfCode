using NUnit.Framework;
using System;

namespace AdventOfCode2017
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day3
    {
        private readonly int realData = 277678;

        [TestCase(1, 0)]
        [TestCase(12, 3)]
        [TestCase(23, 2)]
        [TestCase(1024, 31)]
        [TestCase(null, 475)]
        public void Part1(int? input, int? expected)
        {
            input = input ?? realData;

            var n = 0;

            for (int i = 0; i < input.Value; i++)
            {
                if (i * i > input.Value)
                {
                    n = i;
                    break;
                }
            }

            int[,] matrix = new int[n, n];
            int row = 0;
            int col = 0;
            string direction = "right";

            for (int i = n * n; i > 0; i--)
            {
                if (direction == "right" && (col > n - 1 || matrix[row, col] != 0))
                {
                    direction = "down";
                    col--;
                    row++;
                }
                if (direction == "down" && (row > n - 1 || matrix[row, col] != 0))
                {
                    direction = "left";
                    row--;
                    col--;
                }
                if (direction == "left" && (col < 0 || matrix[row, col] != 0))
                {
                    direction = "up";
                    col++;
                    row--;
                }

                if (direction == "up" && row < 0 || matrix[row, col] != 0)
                {
                    direction = "right";
                    row++;
                    col++;
                }

                matrix[row, col] = i;

                if (direction == "right")
                {
                    col++;
                }
                if (direction == "down")
                {
                    row++;
                }
                if (direction == "left")
                {
                    col--;
                }
                if (direction == "up")
                {
                    row--;
                }
            }

            if (direction == "right" && col > 0)
            {
                col--;
            }
            if (direction == "down" && row > 0)
            {
                row--;
            }
            if (direction == "left")
            {
                col++;
            }
            if (direction == "up")
            {
                row++;
            }

            var inputRow = 0;
            var inputColumn = 0;

            for (int rowIndex = 0; rowIndex < n; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < n; columnIndex++)
                {
                    var item = matrix[rowIndex, columnIndex];
                    if (item == input.Value)
                    {
                        inputRow = rowIndex;
                        inputColumn = columnIndex;
                    }
                }
            }

            var rowDistance = Math.Abs(row - inputRow);
            var columnDistance = Math.Abs(col - inputColumn);
            var result = rowDistance + columnDistance;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}