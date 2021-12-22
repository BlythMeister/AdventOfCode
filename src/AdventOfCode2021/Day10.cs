using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day10
    {
        [TestCase("Day10-Sample", 26397)]
        [TestCase("Day10", 240123)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, false);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day10-Sample", (uint)288957)]
        [TestCase("Day10", 3260812321)]
        public void Part2(string inputFile, uint? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"));
            var result = Execute(lines, true);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private long Execute(string[] inputs, bool scoreAutoComplete)
        {
            var scores = new List<long>();

            foreach (var input in inputs)
            {
                var chars = new List<char>();
                var corruptedLine = false;
                foreach (var c in input)
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        chars.Add(c);
                    }
                    else
                    {
                        var last = chars[chars.Count - 1];
                        chars.RemoveAt(chars.Count - 1);
                        var expected = last switch
                        {
                            '(' => ')',
                            '[' => ']',
                            '{' => '}',
                            '<' => '>',
                            _ => throw new NotImplementedException()
                        };

                        if (c != expected)
                        {
                            corruptedLine = true;
                            if (!scoreAutoComplete)
                            {
                                long points = c switch
                                {
                                    ')' => 3,
                                    ']' => 57,
                                    '}' => 1197,
                                    '>' => 25137,
                                    _ => throw new NotImplementedException()
                                };
                                scores.Add(points);
                            }
                        }
                    }
                }

                if (!corruptedLine && scoreAutoComplete)
                {
                    long lineScore = 0;
                    chars.Reverse();
                    foreach (var c in chars)
                    {
                        lineScore *= 5;

                        long points = c switch
                        {
                            '(' => 1,
                            '[' => 2,
                            '{' => 3,
                            '<' => 4,
                            _ => throw new NotImplementedException()
                        };
                        lineScore += points;
                    }

                    scores.Add(lineScore);
                }
            }

            if (scoreAutoComplete)
            {
                var sorted = scores.OrderBy(x => x).ToList();
                var middle = (int)Math.Floor(sorted.Count / 2d);
                return sorted[middle];
            }
            else
            {
                return scores.Sum();
            }
        }
    }
}
