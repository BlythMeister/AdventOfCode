using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day1
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day1.dat"));

        [TestCase("+1, +1, +1", 3)]
        [TestCase("+1, +1, -2", 0)]
        [TestCase("-1, -2, -3", -6)]
        [TestCase(null, 477)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? input.Split(',') : realData;
            var result = Execute(lines, false);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("+1, -1", 0)]
        [TestCase("+3, +3, +4, -2, -4", 10)]
        [TestCase("-6, +3, +8, +5, -6", 5)]
        [TestCase("+7, +7, -2, -7, -4", 14)]
        [TestCase(null, 390)]
        public void Part2(string input, int? expected)
        {
            var lines = input != null ? input.Split(',') : realData;
            var result = Execute(lines, true);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static int Execute(string[] input, bool returnMatch)
        {
            var result = 0;
            var seenNumbers = new List<int> { 0 };

            while (true)
            {
                foreach (var s in input)
                {
                    var num = int.Parse(s);
                    result += num;

                    if (returnMatch && seenNumbers.Contains(result))
                    {
                        return result;
                    }

                    seenNumbers.Add(result);
                }

                if (!returnMatch) return result;
            }
        }
    }
}
