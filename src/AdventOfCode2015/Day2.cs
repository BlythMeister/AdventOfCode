using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2015
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day2
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day2.dat"));

        [TestCase("2x3x4", 58)]
        [TestCase("1x1x10", 43)]
        [TestCase("2x3x4\n1x1x10", 101)]
        [TestCase(null, 1606483)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var parts = line.Split('x');
                var length = int.Parse(parts[0]);
                var width = int.Parse(parts[1]);
                var height = int.Parse(parts[2]);

                var side1 = length * width;
                var side2 = width * height;
                var side3 = height * length;

                var totalSize = side1 * 2 + side2 * 2 + side3 * 2;
                var slack = (new[] { side1, side2, side3 }).Min();

                result = result + totalSize + slack;
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("2x3x4", 34)]
        [TestCase("1x1x10", 14)]
        [TestCase("2x3x4\n1x1x10", 48)]
        [TestCase(null, 3842356)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var parts = line.Split('x');
                var length = int.Parse(parts[0]);
                var width = int.Parse(parts[1]);
                var height = int.Parse(parts[2]);

                var wrappingRibbon = (new[] { length, width, height }).OrderBy(x => x).Take(2).Select(x => x * 2).Sum();
                var bow = length * width * height;

                result = result + wrappingRibbon + bow;
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}
