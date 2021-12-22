using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day02
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day02.dat"));

        [TestCase("5\t1\t9\t5\n7\t5\t3\n2\t4\t6\t8", 18)]
        [TestCase(null, 54426)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;

            var result = 0;
            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var parts = line.Split(new[] { "\t" }, StringSplitOptions.None);
                var numbers = parts.Select(int.Parse).ToList();

                var max = numbers.Max();
                var min = numbers.Min();
                result += (max - min);
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }


        [TestCase("5\t9\t2\t8\n9\t4\t7\t3\n3\t8\t6\t5", 9)]
        [TestCase(null, 333)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;

            var result = 0;
            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var parts = line.Split(new[] { "\t" }, StringSplitOptions.None);
                var numbers = parts.Select(int.Parse).ToList();

                for (int i = 0; i < numbers.Count; i++)
                {
                    for (int j = 0; j < numbers.Count; j++)
                    {
                        if (i == j) continue;

                        var first = (decimal)numbers[i];
                        var second = (decimal)numbers[j];

                        if (first > second)
                        {
                            var calc = first / second;
                            if ((calc % 1) == 0)
                            {
                                result += (int)calc;
                            }
                        }
                    }
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
