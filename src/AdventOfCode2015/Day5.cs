using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2015
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day5
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day5.dat"));

        [TestCase("ugknbfddgicrmopn", 1)]
        [TestCase("jchzalrnumimnmhp", 0)]
        [TestCase("haegwjzuvuyypxyu", 0)]
        [TestCase("dvszwmarrgswjxmb", 0)]
        [TestCase(null, 258)]
        public void Part1(string input, int? expected)
        {
            input = input ?? realData;
            var result = 0;

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var vowelCount = line.Count(x => x == 'a' || x == 'e' || x == 'i' || x == 'o' || x == 'u');
                if (vowelCount < 3) continue;

                var foundDouble = false;
                for (int i = 0; i < line.Length - 1; i++)
                {
                    var current = line[i];
                    var next = line[i + 1];
                    if (current == next)
                    {
                        foundDouble = true;
                        break;
                    }
                }
                if (!foundDouble) continue;

                if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy")) continue;

                result++;
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("qjhvhtzxzqqjkmpb", 1)]
        [TestCase("xxyxx", 1)]
        [TestCase("uurcxstgmygtbstg", 0)]
        [TestCase("ieodomkazucvgmuy", 0)]
        [TestCase("aaa", 0)]
        [TestCase("abcdefeghi", 0)]
        [TestCase("abcdefeghghi", 1)]
        [TestCase(null, 53)]
        public void Part2(string input, int? expected)
        {
            input = input ?? realData;
            var result = 0;

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                //Now, a nice string is one with all of the following properties:

                //It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy(xy) or aabcdefgaa(aa), but not like aaa(aa, but it overlaps).
                //It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi(efe), or even aaa.

                var foundDouble = false;
                for (int i = 0; i < line.Length - 1; i++)
                {
                    var preStr = line.Substring(0, i);
                    var str = line.Substring(i, 2);
                    var postStr = line.Substring(i + 2);

                    if (preStr.Contains(str) || postStr.Contains(str))
                    {
                        foundDouble = true;
                    }

                }
                if (!foundDouble) continue;

                var foundLetterLoop = false;
                for (int i = 0; i < line.Length - 2; i++)
                {
                    var current = line[i];
                    var next = line[i + 2];

                    if (current == next)
                    {
                        foundLetterLoop = true;
                        break;
                    }
                }
                if (!foundLetterLoop) continue;

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

