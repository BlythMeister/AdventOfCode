using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day8
    {
        [TestCase("Day8-Sample", 26)]
        [TestCase("Day8", 390)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute1(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day8-Sample", 61229)]
        [TestCase("Day8", 1011785)]
        public void Part2(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute2(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static int Execute1(string[] inputs)
        {
            var numbers = new List<HashSet<char>>();
            foreach (var input in inputs)
            {
                var part = input.Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var outputNumbers = part[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => x.OrderBy(v => v).ToHashSet());
                numbers.AddRange(outputNumbers);
            }

            return numbers.Count(x => x.Count == 2) +
                       numbers.Count(x => x.Count == 3) +
                       numbers.Count(x => x.Count == 4) +
                       numbers.Count(x => x.Count == 7);
        }

        private static int Execute2(string[] inputs)
        {
            var sum = 0;
            foreach (var input in inputs)
            {
                var part = input.Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var calculation = part[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => x.OrderBy(v => v).ToHashSet()).ToList();
                var values = part[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => x.OrderBy(v => v).ToHashSet()).ToList();

                var oneValue = calculation.First(x => x.Count == 2);
                var fourValue = calculation.First(x => x.Count == 4);
                var sevenValue = calculation.First(x => x.Count == 3);
                var eightValue = calculation.First(x => x.Count == 7);
                var nineValue = calculation.First(x => x.Count == 6 && fourValue.All(x.Contains));
                var zeroValue = calculation.First(x => x.Count == 6 && !fourValue.All(x.Contains) && oneValue.All(x.Contains));
                var sixValue = calculation.First(x => x.Count == 6 && !fourValue.All(x.Contains) && !oneValue.All(x.Contains));
                var threeValue = calculation.First(x => x.Count == 5 && oneValue.All(x.Contains));
                var fiveValue = calculation.First(x => x.Count == 5 && !oneValue.All(x.Contains) && x.All(sixValue.Contains));
                var twoValue = calculation.First(x => x.Count == 5 && !oneValue.All(x.Contains) && !x.All(sixValue.Contains));

                var number = string.Empty;
                foreach (var value in values)
                {
                    if (value.SequenceEqual(zeroValue))
                    {
                        number += "0";
                    }

                    if (value.SequenceEqual(oneValue))
                    {
                        number += "1";
                    }

                    if (value.SequenceEqual(twoValue))
                    {
                        number += "2";
                    }

                    if (value.SequenceEqual(threeValue))
                    {
                        number += "3";
                    }

                    if (value.SequenceEqual(fourValue))
                    {
                        number += "4";
                    }

                    if (value.SequenceEqual(fiveValue))
                    {
                        number += "5";
                    }

                    if (value.SequenceEqual(sixValue))
                    {
                        number += "6";
                    }

                    if (value.SequenceEqual(sevenValue))
                    {
                        number += "7";
                    }

                    if (value.SequenceEqual(eightValue))
                    {
                        number += "8";
                    }

                    if (value.SequenceEqual(nineValue))
                    {
                        number += "9";
                    }
                }

                sum += int.Parse(number);
            }

            return sum;
        }
    }
}
