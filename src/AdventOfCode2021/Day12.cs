using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day12
    {
        [TestCase("Day12-Sample1", 10)]
        [TestCase("Day12-Sample2", 19)]
        [TestCase("Day12-Sample3", 226)]
        [TestCase("Day12", 3369)]
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

        [TestCase("Day12-Sample1", 36)]
        [TestCase("Day12-Sample2", 103)]
        [TestCase("Day12-Sample3", 3509)]
        [TestCase("Day12", 85883)]
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

        private static long Execute(string[] inputs, bool visitOneSmallCaveTwice)
        {
            var paths = new List<List<string>>();

            var startConnections = GetConnections(inputs, "start").ToList();
            foreach (var connection in startConnections)
            {
                paths.Add(new List<string> { "start", connection });
            }

            while (!paths.All(x => x.Contains("end")))
            {
                var newPaths = new List<List<string>>();
                newPaths.AddRange(paths.Where(x => x.Contains("end")));
                foreach (var path in paths.Where(x => !x.Contains("end")))
                {
                    var connections = GetConnections(inputs, path.Last()).ToList();
                    foreach (var connection in connections)
                    {
                        var maxSmallCaveTimes = 1;
                        if (visitOneSmallCaveTwice)
                        {
                            var smallCavesVisited = path.Where(x => x == x.ToLower()).GroupBy(x => x);
                            if (!smallCavesVisited.Any(x => x.Count() > 1))
                            {
                                maxSmallCaveTimes = 2;
                            }
                        }
                        var isSmallCave = connection == connection.ToLower() && connection != "start" && connection != "end";
                        var countOfSmallCave = isSmallCave ? path.Count(x => x == connection) : -1;
                        var isStart = connection == "start";
                        if (isStart)
                        {
                            //ignore
                        }
                        else if (isSmallCave && countOfSmallCave >= maxSmallCaveTimes)
                        {
                            //ignore
                        }
                        else
                        {
                            var newPath = path.ToList();
                            newPath.Add(connection);
                            newPaths.Add(newPath);
                        }
                    }
                }

                paths = newPaths;
            }

            return paths.Count;
        }

        private static IEnumerable<string> GetConnections(string[] connections, string source)
        {
            foreach (var input in connections)
            {
                var route = input.Split('-');
                if (route[0] == source)
                {
                    yield return route[1];
                }

                if (route[1] == source)
                {
                    yield return route[0];
                }
            }
        }
    }
}
