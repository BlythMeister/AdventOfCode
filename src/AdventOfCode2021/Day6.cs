using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day6
    {
        [TestCase("Day6-Sample", 18, 26)]
        [TestCase("Day6", 80, 377263)]
        public void Part1(string inputFile, int days, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"))[0].Split(",");
            var result = Execute(lines, days);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day6", 256, 1695929023803)]
        public void Part2(string inputFile, int days, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"))[0].Split(",");
            var result = Execute(lines, days);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private long Execute(string[] inputs, int days)
        {
            var initial = inputs.Select(int.Parse).ToArray();
            var newbirths = new long[days];

            IEnumerable<int> births(int initialAge, int daysLeft) => Enumerable.Range(0, 50).Select(x => x * 7 + initialAge).Where(x => x < daysLeft);

            foreach (var age in initial)
            {
                foreach (var birthday in births(age, days))
                {
                    newbirths[birthday] += 1;
                }
            }

            for (var day = 0; day < days; day++)
            {
                var newBorn = newbirths[day];

                foreach (var birthday in births(8, days - day - 1))
                {
                    newbirths[day + 1 + birthday] += newBorn;
                }
            }

            return initial.Length + newbirths.Sum();
        }
    }
}
