using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day2
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day2.dat"));

        [TestCase("1-3 a: abcde", 1)]
        [TestCase("1-3 b: cdefg", 0)]
        [TestCase("2-9 c: ccccccccc", 1)]
        [TestCase(null, 580)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? new[] { input } : realData;
            var result = Execute(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("1-3 a: abcde", 1)]
        [TestCase("1-3 b: cdefg", 0)]
        [TestCase("2-9 c: ccccccccc", 0)]
        [TestCase(null, 611)]
        public void Part2(string input, int? expected)
        {
            var lines = input != null ? new[] { input } : realData;
            var result = Execute2(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static int Execute(string[] inputs)
        {
            var validPasswords = 0;

            foreach (var input in inputs)
            {
                var inputParts = input.Split(' ');

                var range = inputParts[0].Split('-');
                var min = int.Parse(range[0]);
                var max = int.Parse(range[1]);

                var letter = inputParts[1].First();
                var occurrences = inputParts[2].Count(inputLetter => inputLetter == letter);

                if (occurrences >= min && occurrences <= max)
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }

        private static int Execute2(string[] inputs)
        {
            var validPasswords = 0;

            foreach (var input in inputs)
            {
                var inputParts = input.Split(' ');

                var range = inputParts[0].Split('-');
                var firstPos = int.Parse(range[0]) - 1;
                var secondPos = int.Parse(range[1]) - 1;

                var letter = inputParts[1].First();

                var firstMatch = inputParts[2][firstPos] == letter;
                var secondMatch = inputParts[2][secondPos] == letter;

                if (firstMatch  || secondMatch)
                {
                    if (!(firstMatch && secondMatch))
                    {
                        validPasswords++;
                    }
                }
            }

            return validPasswords;
        }
    }
}
