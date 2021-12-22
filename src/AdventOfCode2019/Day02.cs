using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day02
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day02.dat"));

        [TestCase("1,0,0,0,99", 0, 0, 0, 2)]
        [TestCase("2,3,0,3,99", 3, 3, 0, 6)]
        [TestCase(null, 0, 12, 2, 4138687)]
        public void Part1(string input, int expectedPosition, int noun, int verb, int? expected)
        {
            var lines = input != null ? input : realData[0];
            var result = Execute(lines.Split(',').Select(x => int.Parse(x)).ToArray(), expectedPosition, noun, verb);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("1,0,0,0,99", 0, 0, 0, 2, 0)]
        [TestCase("2,3,0,3,99", 3, 3, 0, 6, 300)]
        [TestCase(null, 0, null, null, 19690720, 6635)]
        public void Part2(string input, int expectedPosition, int? noun, int? verb, int expected, int? expectedNounValue)
        {
            var lines = input != null ? input : realData[0];
            var result = -1;

            var nounToUse = noun == null ? 0 : noun.Value;
            var verbToUse = verb == null ? 0 : verb.Value;

            while (result != expected)
            {
                result = Execute(lines.Split(',').Select(x => int.Parse(x)).ToArray(), expectedPosition, nounToUse, verbToUse);
                if (result != expected)
                {
                    if (verbToUse == 99)
                    {
                        nounToUse++;
                        verbToUse = 0;
                    }
                    else
                    {
                        verbToUse++;
                    }
                }
            }

            if (expectedNounValue != null)
            {
                Assert.AreEqual(expectedNounValue.Value, 100 * nounToUse + verbToUse);
            }
            Console.WriteLine(result);
        }

        private static int Execute(int[] inputs, int expectedPosition, int noun, int verb)
        {
            var pos = 0;
            inputs[1] = noun;
            inputs[2] = verb;

            while (true)
            {
                var opCode = inputs[pos];

                if (opCode == 99)
                {
                    return inputs[expectedPosition];
                }

                var posSelect1 = inputs[pos + 1];
                var posSelect2 = inputs[pos + 2];
                var posSelect3 = inputs[pos + 3];
                var pos1 = inputs[posSelect1];
                var pos2 = inputs[posSelect2];

                if (opCode == 1)
                {
                    inputs[posSelect3] = pos1 + pos2;
                    pos += 4;
                }
                else if (opCode == 2)
                {
                    inputs[posSelect3] = pos1 * pos2;
                    pos += 4;
                }
                else
                {
                    throw new Exception($"Unknown opCode {opCode}");
                }
            }
        }
    }
}
