using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2015
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day1
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day1.dat"));

        [TestCase("(())", 0)]
        [TestCase("()()", 0)]
        [TestCase("(((", 3)]
        [TestCase("(()(()(", 3)]
        [TestCase("))(((((", 3)]
        [TestCase(null, 138)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            foreach (var character in input)
            {
                if (character == '(')
                {
                    result++;
                }
                else
                {
                    result--;
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase(")", 1)]
        [TestCase("()())", 5)]
        [TestCase(null, 1771)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = 1;
            var position = 0;

            foreach (var character in input)
            {
                if (character == '(')
                {
                    position++;
                }
                else
                {
                    position--;
                }

                if (position < 0)
                {
                    break;
                }

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
