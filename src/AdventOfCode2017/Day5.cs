using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day5
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day5.dat"));

        [TestCase("0\n3\n0\n1\n-3", 5)]
        [TestCase(null, 396086)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;
            var jumps = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Select(x => int.Parse(x)).ToList();
            var position = 0;
            var result = 0;

            while (position >= 0 && position < jumps.Count)
            {
                var value = jumps[position];
                jumps[position] = value + 1;
                position += value;
                result++;
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("0\n3\n0\n1\n-3", 10, Explicit = true)]
        [TestCase(null, 28675390, Explicit = true)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var jumps = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Select(x => int.Parse(x)).ToList();
            var position = 0;
            var result = 0;

            while (position >= 0 && position < jumps.Count)
            {
                var value = jumps[position];
                if (value >= 3)
                {
                    jumps[position] = value - 1;
                }
                else
                {
                    jumps[position] = value + 1;
                }
                position += value;
                result++;
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}