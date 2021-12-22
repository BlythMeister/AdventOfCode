using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day11
    {
        [TestCase("Day11-Sample", 1656)]
        [TestCase("Day11", 1608)]
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

        [TestCase("Day11-Sample", 195)]
        [TestCase("Day11", 214)]
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

        private static long Execute(string[] inputs, bool allFlash)
        {
            var octopus = new int[10, 10];
            for (var i = 0; i < inputs.Length; i++)
            {
                for (var j = 0; j < inputs[i].Length; j++)
                {
                    octopus[i, j] = int.Parse(inputs[i][j].ToString());
                }
            }

            long totalFlashes = 0;
            long step = 0;
            while (true)
            {
                step++;

                var flashed = new List<string>();

                for (var i = 0; i < inputs.Length; i++)
                {
                    for (var j = 0; j < inputs[i].Length; j++)
                    {
                        octopus[i, j]++;
                    }
                }

                for (var i = 0; i < inputs.Length; i++)
                {
                    for (var j = 0; j < inputs[i].Length; j++)
                    {
                        if (octopus[i, j] > 9 && !flashed.Contains($"{i},{j}"))
                        {
                            flashed.Add($"{i},{j}");
                        }
                    }
                }

                var newFlashes = flashed.ToList(); //ToList to get a new instance
                while (newFlashes.Any())
                {
                    foreach (var newFlash in newFlashes.Select(flash => flash.Split(",").Select(int.Parse).ToList()))
                    {
                        if (newFlash[0] > 0)
                        {
                            if (newFlash[1] > 0)
                            {
                                octopus[newFlash[0] - 1, newFlash[1] - 1]++;
                            }

                            octopus[newFlash[0] - 1, newFlash[1]]++;

                            if (newFlash[1] < 9)
                            {
                                octopus[newFlash[0] - 1, newFlash[1] + 1]++;
                            }
                        }

                        if (newFlash[1] > 0)
                        {
                            octopus[newFlash[0], newFlash[1] - 1]++;
                        }

                        if (newFlash[1] < 9)
                        {
                            octopus[newFlash[0], newFlash[1] + 1]++;
                        }

                        if (newFlash[0] < 9)
                        {
                            if (newFlash[1] > 0)
                            {
                                octopus[newFlash[0] + 1, newFlash[1] - 1]++;
                            }

                            octopus[newFlash[0] + 1, newFlash[1]]++;

                            if (newFlash[1] < 9)
                            {
                                octopus[newFlash[0] + 1, newFlash[1] + 1]++;
                            }
                        }
                    }

                    newFlashes.Clear();
                    for (var i = 0; i < inputs.Length; i++)
                    {
                        for (var j = 0; j < inputs[i].Length; j++)
                        {
                            if (octopus[i, j] > 9 && !flashed.Contains($"{i},{j}"))
                            {
                                newFlashes.Add($"{i},{j}");
                            }
                        }
                    }

                    flashed.AddRange(newFlashes);
                }

                foreach (var flash in flashed.Select(flash => flash.Split(",").Select(int.Parse).ToList()))
                {
                    octopus[flash[0], flash[1]] = 0;
                    totalFlashes++;
                }

                if (allFlash)
                {
                    if (flashed.Count == octopus.Length)
                    {
                        return step;
                    }
                }
                else
                {
                    if (step == 100)
                    {
                        return totalFlashes;
                    }
                }
            }
        }
    }
}
