using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day01
    {
        [TestCase("Day01-Sample", 24000)]
        [TestCase("Day01", 70613)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, 1);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day01-Sample", 45000)]
        [TestCase("Day01", 205805)]
        public void Part2(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, 3);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static long Execute(string[] inputs, int topX)
        {
            var cals = new List<int>();
            var calc = 0;
            foreach (var input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    cals.Add(calc);
                    calc = 0;
                }
                else
                {
                    calc += int.Parse(input);
                }
            }

            if (calc > 0)
            {
                cals.Add(calc);
            }

            return cals.OrderByDescending(x => x).Take(topX).Sum();
        }
    }
}
