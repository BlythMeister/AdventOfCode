using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCodex
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Dayx
    {
        [TestCase("Dayx-Sample", 0)]
        [TestCase("Dayx", 0)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        ////[TestCase("Dayx-Sample", 0)]
        ////[TestCase("Dayx", 0)]
        ////public void Part2(string inputFile, long? expected)
        ////{
        ////    var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
        ////    var result = Execute(lines);
		////
        ////    if (expected != null)
        ////    {
        ////        Assert.AreEqual(expected.Value, result);
        ////    }
        ////    Console.WriteLine(result);
        ////}

        private static long Execute(string[] inputs)
        {
            return 0;
        }
    }
}
