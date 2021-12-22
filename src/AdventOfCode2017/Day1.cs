using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2017
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day1
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day1.dat"));

        [TestCase("1122", 3)]
        [TestCase("1111", 4)]
        [TestCase("1234", 0)]
        [TestCase("91212129", 9)]
        [TestCase(null, 1089)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;
            var result = Execute(input, 1);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("1212", 6)]
        [TestCase("1221", 0)]
        [TestCase("123425", 4)]
        [TestCase("123123", 12)]
        [TestCase("12131415", 4)]
        [TestCase(null, 1156)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = Execute(input, input.Length / 2);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static int Execute(string input, int stepsAhead)
        {
            var result = 0;

            for (var i = 0; i < input.Length; i++)
            {
                int comparePart;
                if (i + stepsAhead >= input.Length)
                {
                    var positionToGet = (i + stepsAhead) - input.Length;
                    comparePart = int.Parse(input[positionToGet].ToString());
                }
                else
                {
                    comparePart = int.Parse(input[i + stepsAhead].ToString());
                }

                var part = int.Parse(input[i].ToString());

                if (part == comparePart)
                {
                    result += part;
                }
            }

            return result;
        }
    }
}
