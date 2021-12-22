using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            input ??= realData;

            var n = 0;

            for (int i = 0; i < input.Value; i++)
            {
                if (i * i > input.Value)
                {
                    n = i;
                    break;
                }
            }

            n += 3;

            var matrix = new int[n, n];
            var direction = "right";
            var currentRow = (int)Math.Floor(n / 2d);
            var currentCol = (int)Math.Floor(n / 2d);
            var currentNumber = 1;
            matrix[currentRow, currentCol] = currentNumber;
            currentNumber++;

            while (currentNumber <= input)
            {
                if (direction == "right")
                {
                    currentCol++;
                }
                else if (direction == "up")
                {
                    currentRow--;
                }
                else if (direction == "left")
                {
                    currentCol--;
                }
                else if (direction == "down")
                {
                    currentRow++;
                }

                matrix[currentRow, currentCol] = currentNumber;
                currentNumber++;

                if (direction == "right" && matrix[currentRow - 1, currentCol] == 0)
                {
                    direction = "up";
                }
                else if (direction == "up" && matrix[currentRow, currentCol - 1] == 0)
                {
                    direction = "left";
                }
                else if (direction == "left" && matrix[currentRow + 1, currentCol] == 0)
                {
                    direction = "down";
                }
                else if (direction == "down" && matrix[currentRow, currentCol + 1] == 0)
                {
                    direction = "right";
                }
            }

            var startRow = (int)Math.Floor(n / 2d);
            var startCol = (int)Math.Floor(n / 2d);
            var rowDistance = Math.Abs(startRow - currentRow);
            var columnDistance = Math.Abs(startCol - currentCol);
            var result = rowDistance + columnDistance;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase(1, 2)]
        [TestCase(5, 10)]
        [TestCase(10, 11)]
        [TestCase(700, 747)]
        [TestCase(null, 279138)]
        public void Part2(int? input, int? expected)
        {
            input ??= realData;

            var n = 0;

            for (int i = 0; i < input.Value; i++)
            {
                if (i * i > input.Value)
                {
                    n = i;
                    break;
                }
            }

            n += 3;

            var matrix = new int[n, n];
            var direction = "right";
            var currentRow = (int)Math.Floor(n / 2d);
            var currentCol = (int)Math.Floor(n / 2d);
            var currentNumber = 1;
            matrix[currentRow, currentCol] = currentNumber;

            while (currentNumber <= input)
            {
                if (direction == "right")
                {
                    currentCol++;
                }
                else if (direction == "up")
                {
                    currentRow--;
                }
                else if (direction == "left")
                {
                    currentCol--;
                }
                else if (direction == "down")
                {
                    currentRow++;
                }

                var positionsToAdd = new List<Tuple<int, int>>
                {
                    new Tuple<int, int>(currentRow - 1, currentCol -1),
                    new Tuple<int, int>(currentRow - 1, currentCol),
                    new Tuple<int, int>(currentRow - 1, currentCol + 1),
                    new Tuple<int, int>(currentRow, currentCol -1),
                    new Tuple<int, int>(currentRow, currentCol + 1),
                    new Tuple<int, int>(currentRow + 1, currentCol -1),
                    new Tuple<int, int>(currentRow + 1, currentCol),
                    new Tuple<int, int>(currentRow + 1, currentCol + 1)
                };

                var newNumber = 0;
                foreach (var position in positionsToAdd)
                {
                    try
                    {
                        var item = matrix[position.Item1, position.Item2];
                        newNumber += item;
                    }
                    catch (Exception)
                    {
                        //Ignore
                    }
                }

                matrix[currentRow, currentCol] = newNumber;
                currentNumber = newNumber;

                if (direction == "right" && matrix[currentRow - 1, currentCol] == 0)
                {
                    direction = "up";
                }
                else if (direction == "up" && matrix[currentRow, currentCol - 1] == 0)
                {
                    direction = "left";
                }
                else if (direction == "left" && matrix[currentRow + 1, currentCol] == 0)
                {
                    direction = "down";
                }
                else if (direction == "down" && matrix[currentRow, currentCol + 1] == 0)
                {
                    direction = "right";
                }
            }

            var result = currentNumber;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}
