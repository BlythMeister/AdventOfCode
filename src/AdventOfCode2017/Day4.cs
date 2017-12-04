using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day4
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day4.dat"));

        [TestCase("aa bb cc dd ee", 1)]
        [TestCase("aa bb cc dd aa", 0)]
        [TestCase("aa bb cc dd aaa", 1)]
        [TestCase(null, 325)]
        public void Part1(string input, int? expected)
        {
            input = input ?? realData;

            var result = 0;
            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var parts = line.Split(new[] { " " }, StringSplitOptions.None);
                var distinctParts = parts.Distinct();

                if (parts.Length == distinctParts.Count())
                {
                    result++;
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("abcde fghij", 1)]
        [TestCase("abcde xyz ecdab", 0)]
        [TestCase("a ab abc abd abf abj", 1)]
        [TestCase("iiii oiii ooii oooi oooo", 1)]
        [TestCase("oiii ioii iioi iiio", 0)]
        [TestCase(null, 119)]
        public void Part2(string input, int? expected)
        {
            input = input ?? realData;

            var result = 0;
            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var parts = line.Split(new[] { " " }, StringSplitOptions.None);
                var partsOrdered = new List<string>();

                foreach (var part in parts)
                {
                    var chars = part.ToLower().ToCharArray();
                    Array.Sort(chars);
                    partsOrdered.Add(new string(chars));
                }

                var distinctParts = partsOrdered.Distinct().ToList();

                if (partsOrdered.Count() == distinctParts.Count())
                {
                    result++;
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}