using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day1
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day1.dat"));

        [TestCase("199,200,208,210,200,207,240,269,260,263", 7)]
        [TestCase(null, 1342)]
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

        [TestCase("199,200,208,210,200,207,240,269,260,263", 5)]
        [TestCase(null, 1378)]
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

        private int Execute(string[] inputs, bool compare3)
        {
            var increases = 0;
            for (var i = 0; i < inputs.Length; i++)
            {
                int previousNumber, number;
                if (compare3)
                {
                    previousNumber = i < 3 ? int.MaxValue : int.Parse(inputs[i - 3]) + int.Parse(inputs[i - 2]) + int.Parse(inputs[i - 1]);
                    number = i < 2 ? int.MaxValue : int.Parse(inputs[i - 2]) + int.Parse(inputs[i - 1]) + int.Parse(inputs[i]);
                }
                else
                {
                    previousNumber = i < 1 ? int.MaxValue : int.Parse(inputs[i - 1]);
                    number = int.Parse(inputs[i]);
                }

                if (number > previousNumber)
                {
                    increases++;
                }
            }

            return increases;
        }
    }
}
