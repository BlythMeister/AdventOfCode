using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day06
    {
        private readonly string realData = "0,5,10,0,11,14,13,4,11,8,8,7,1,4,12,11";

        [TestCase("0,2,7,0", 5)]
        [TestCase(null, 7864)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;
            var result = 0;
            var seenSeqs = new HashSet<string> { input };
            while (true)
            {
                var parts = seenSeqs.Last().Split(',').Select(int.Parse).ToList();
                var workingNumber = parts.Max();
                var startPoint = parts.IndexOf(workingNumber);
                parts[startPoint] = 0;

                while (workingNumber > 0)
                {
                    startPoint++;
                    if (startPoint >= parts.Count) startPoint = 0;
                    parts[startPoint]++;
                    workingNumber--;
                }

                var newSeq = string.Join(",", parts);
                if (!seenSeqs.Add(newSeq))
                {
                    result = seenSeqs.Count;
                    break;
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("0,2,7,0", 4)]
        [TestCase(null, 1695)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = 0;
            var seenSeqs = new HashSet<string> { input };
            while (true)
            {
                var parts = seenSeqs.Last().Split(',').Select(int.Parse).ToList();
                var workingNumber = parts.Max();
                var startPoint = parts.IndexOf(workingNumber);
                parts[startPoint] = 0;

                while (workingNumber > 0)
                {
                    startPoint++;
                    if (startPoint >= parts.Count) startPoint = 0;
                    parts[startPoint]++;
                    workingNumber--;
                }

                var newSeq = string.Join(",", parts);
                if (seenSeqs.Add(newSeq))
                {
                    continue;
                }

                var index = 0;
                foreach (var b in seenSeqs)
                {
                    if (b.Equals(newSeq))
                    {
                        result = seenSeqs.Count - index;
                        break;
                    }
                    index++;
                }
                break;
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}