using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day09
    {
        [TestCase("Day09-Sample", 15)]
        [TestCase("Day09", 425)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, false);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day09-Sample", 1134)]
        [TestCase("Day09", 1135260)]
        public void Part2(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, true);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private long Execute(string[] inputs, bool basins)
        {
            var map = new int[inputs.Length, inputs[0].Length];

            for (var i = 0; i < inputs.Length; i++)
            {
                for (var j = 0; j < inputs[0].Length; j++)
                {
                    map[i, j] = int.Parse(inputs[i][j].ToString());
                }
            }

            var risk = 0;
            var basinsSizes = new List<int>();
            for (var i = 0; i < inputs.Length; i++)
            {
                for (var j = 0; j < inputs[0].Length; j++)
                {
                    var myValue = map[i, j];
                    var hasLower = false;
                    if (i > 0 && myValue >= map[i - 1, j])
                    {
                        hasLower = true;
                    }
                    else if (j > 0 && myValue >= map[i, j - 1])
                    {
                        hasLower = true;
                    }
                    else if (i < inputs.Length - 1 && myValue >= map[i + 1, j])
                    {
                        hasLower = true;
                    }
                    else if (j < inputs[0].Length - 1 && myValue >= map[i, j + 1])
                    {
                        hasLower = true;
                    }

                    if (!hasLower)
                    {
                        risk += myValue + 1;
                        var basinLocations = new List<string>();
                        RecursiveBasinSize(basinLocations, map, i, j, inputs.Length - 1, inputs[0].Length - 1);
                        basinsSizes.Add(basinLocations.Count);
                    }
                }
            }

            if (basins)
            {
                return basinsSizes.OrderByDescending(x => x).Take(3).Aggregate(1, (current, i) => current * i);
            }
            return risk;
        }

        private void RecursiveBasinSize(List<string> visited, int[,] map, int i, int j, int iMax, int jMax)
        {
            var location = $"{i},{j}";

            if (i < 0 || i > iMax || j < 0 || j > jMax)
            {
                return;
            }

            if (visited.Contains(location))
            {
                return;
            }

            if (map[i, j] == 9)
            {
                return;
            }

            visited.Add(location);
            RecursiveBasinSize(visited, map, i + 1, j, iMax, jMax);
            RecursiveBasinSize(visited, map, i - 1, j, iMax, jMax);
            RecursiveBasinSize(visited, map, i, j + 1, iMax, jMax);
            RecursiveBasinSize(visited, map, i, j - 1, iMax, jMax);
        }
    }
}
