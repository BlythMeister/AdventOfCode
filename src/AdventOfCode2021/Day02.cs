using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day02
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day02.dat"));

        [TestCase("forward 5,down 5,forward 8,up 3,down 8,forward 2", 150)]
        [TestCase(null, 2019945)]
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

        [TestCase("forward 5,down 5,forward 8,up 3,down 8,forward 2", 900)]
        [TestCase(null, 1599311480)]
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

        private static int Execute(string[] inputs, bool includeAim)
        {
            var position = 0;
            var depth = 0;
            var aim = 0;

            foreach (var input in inputs)
            {
                if (input.StartsWith("forward"))
                {
                    var value = int.Parse(input[7..].Trim());
                    position += value;
                    if (includeAim)
                    {
                        depth += aim * value;
                    }
                }
                else if (input.StartsWith("up"))
                {
                    if (includeAim)
                    {
                        aim -= int.Parse(input[2..].Trim());
                    }
                    else
                    {
                        depth -= int.Parse(input[2..].Trim());
                    }
                }
                else if (input.StartsWith("down"))
                {
                    if (includeAim)
                    {
                        aim += int.Parse(input[4..].Trim());
                    }
                    else
                    {
                        depth += int.Parse(input[4..].Trim());
                    }
                }
            }

            return position * depth;
        }
    }
}
