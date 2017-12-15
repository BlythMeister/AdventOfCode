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
    }
}

