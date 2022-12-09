using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day03
    {
        [TestCase("Day03-Sample", 157)]
        [TestCase("Day03", 7990)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute1(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day03-Sample", 70)]
        [TestCase("Day03", 2602)]
        public void Part2(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute2(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static long Execute1(string[] inputs)
        {
            var sum = 0;

            foreach (var input in inputs)
            {
                var c1 = input.Take(input.Length / 2);
                var c2 = input.TakeLast(input.Length / 2);
                foreach (var both in c1.Where(x => c2.Contains(x)).Distinct())
                {
                    sum += GetIndex(both);
                }
            }

            return sum;
        }

        private static long Execute2(string[] inputs)
        {
            var sum = 0;
            var group = new List<string>();

            foreach (var input in inputs)
            {
                group.Add(input);
                if (group.Count == 3)
                {
                    foreach (var g1 in group[0])
                    {
                        if (group[1].Contains(g1))
                        {
                            if (group[2].Contains(g1))
                            {
                                sum += GetIndex(g1);
                                break;
                            }
                        }
                    }

                    group.Clear();
                }
            }

            return sum;
        }

        private static int GetIndex(char c)
        {
            var index = char.ToUpper(c) - 64;
            if (char.IsUpper(c))
            {
                index += 26;
            }

            return index;
        }
    }
}
