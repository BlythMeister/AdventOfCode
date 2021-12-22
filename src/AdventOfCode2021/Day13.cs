using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day13
    {
        [TestCase("Day13-Sample", 17)]
        [TestCase("Day13", 745)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, 1, false);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day13-Sample")]
        [TestCase("Day13")]
        public void Part2(string inputFile)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, -1, true);

            Console.WriteLine("^^ Printed Text Is The Answer ^^");
        }

        private long Execute(string[] inputs, int noOfFolds, bool printOutput)
        {
            var (coordinates, folds) = Parse(inputs);
            var (map, maxX, maxY) = GetEmptyMap(coordinates);

            foreach (var coordinate in coordinates)
            {
                map[coordinate.x, coordinate.y] = '#';
            }

            var foldsDone = 0;
            foreach (var fold in folds)
            {
                if (noOfFolds > 0 && foldsDone >= noOfFolds)
                {
                    break;
                }

                if (fold.axis.Equals("X", StringComparison.InvariantCultureIgnoreCase))
                {
                    for (var x = fold.location; x < maxX; x++)
                    {
                        for (var y = 0; y < maxY; y++)
                        {
                            if (map[x, y] == '#')
                            {
                                map[fold.location + fold.location - x, y] = map[x, y];
                            }
                            map[x, y] = 'x';
                        }
                    }
                }
                else
                {
                    for (var x = 0; x < maxX; x++)
                    {
                        for (var y = fold.location; y < maxY; y++)
                        {
                            if (map[x, y] == '#')
                            {
                                map[x, fold.location + fold.location - y] = map[x, y];
                            }
                            map[x, y] = 'x';
                        }
                    }
                }

                foldsDone++;
            }

            if (printOutput)
            {
                PrintMap(map, maxX, maxY);
                return -1;
            }

            var marks = 0;
            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    if (map[x, y] == '#')
                    {
                        marks++;
                    }
                }
            }
            return marks;
        }

        private void PrintMap(char[,] map, int maxX, int maxY)
        {
            for (var y = 0; y < maxY; y++)
            {
                var line = string.Empty;
                for (var x = 0; x < maxX; x++)
                {
                    if (map[x, y] != 'x')
                    {
                        if (map[x, y] == '.')
                        {
                            line += " ";
                        }
                        else
                        {
                            line += map[x, y];
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine(line);
                }
            }
        }

        private (char[,] map, int maxX, int maxY) GetEmptyMap(List<(int x, int y)> coordinates)
        {
            var maxX = coordinates.Max(x => x.x) + 1;
            var maxY = coordinates.Max(x => x.y) + 1;
            var map = new char[maxX, maxY];

            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    map[x, y] = '.';
                }
            }

            return (map, maxX, maxY);
        }

        private (List<(int x, int y)> coordinates, List<(string axis, int location)> folds) Parse(string[] inputs)
        {
            var coordinates = new List<(int x, int y)>();
            var folds = new List<(string axis, int location)>();

            foreach (var input in inputs)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    if (input.StartsWith("fold along"))
                    {
                        var values = input.Substring(11).Trim().Split("=").ToList();
                        folds.Add((values[0], int.Parse(values[1])));
                    }
                    else
                    {
                        var values = input.Split(",").Select(int.Parse).ToList();
                        coordinates.Add((values[0], values[1]));
                    }
                }
            }

            return (coordinates, folds);
        }
    }
}
