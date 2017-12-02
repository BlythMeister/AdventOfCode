using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2016
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day5
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day5.dat"));

        //[TestCase(null, 1089)]
        public void Part1(string input, int? expected)
        {
            input = input ?? realData;
            var result = 0;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        //[TestCase(null, 1156)]
        public void Part2(string input, int? expected)
        {
            input = input ?? realData;
            var result = 0;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}
