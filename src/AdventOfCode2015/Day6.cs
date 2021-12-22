using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2015
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day6
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day6.dat"));

        [TestCase("turn on 0,0 through 999,999", 1000000)]
        [TestCase("toggle 0,0 through 999,0", 1000)]
        [TestCase("turn on 0,0 through 999,999\nturn off 499,499 through 500,500", 999996)]
        [TestCase(null, 400410, Explicit = true)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            var grid = new Dictionary<(int, int), bool>();
            for (int i = 0; i <= 999; i++)
            {
                for (int j = 0; j <= 999; j++)
                {
                    grid.Add((i, j), false);
                }
            }

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                string action;
                if (line.StartsWith("turn on"))
                {
                    action = "turn on";
                }
                else if (line.StartsWith("turn off"))
                {
                    action = "turn off";
                }
                else
                {
                    action = "toggle";
                }

                var remainingText = line[(action.Length + 1)..];
                var start = remainingText[..remainingText.IndexOf("through")].Trim().Split(',').Select(int.Parse).ToList();
                var end = remainingText[(remainingText.IndexOf("through") + 8)..].Trim().Split(',').Select(int.Parse).ToList();

                for (int i = start[0]; i <= end[0]; i++)
                {
                    for (int j = start[1]; j <= end[1]; j++)
                    {
                        switch (action)
                        {
                            case "turn on":
                                grid[(i, j)] = true;
                                break;

                            case "turn off":
                                grid[(i, j)] = false;
                                break;

                            case "toggle":
                                grid[(i, j)] = !grid[(i, j)];
                                break;
                        }
                    }
                }
            }

            result = grid.Count(x => x.Value);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("turn on 0,0 through 999,999", 1000000)]
        [TestCase("toggle 0,0 through 999,0", 2000)]
        [TestCase("toggle 0,0 through 999,999", 2000000)]
        [TestCase("turn on 0,0 through 999,999\nturn off 499,499 through 500,500", 999996)]
        [TestCase(null, 15343601, Explicit = true)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            var grid = new Dictionary<(int, int), int>();
            for (int i = 0; i <= 999; i++)
            {
                for (int j = 0; j <= 999; j++)
                {
                    grid.Add((i, j), 0);
                }
            }

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                string action;
                if (line.StartsWith("turn on"))
                {
                    action = "turn on";
                }
                else if (line.StartsWith("turn off"))
                {
                    action = "turn off";
                }
                else
                {
                    action = "toggle";
                }

                var remainingText = line[(action.Length + 1)..];
                var start = remainingText[..remainingText.IndexOf("through")].Trim().Split(',').Select(int.Parse).ToList();
                var end = remainingText[(remainingText.IndexOf("through") + 8)..].Trim().Split(',').Select(int.Parse).ToList();

                for (int i = start[0]; i <= end[0]; i++)
                {
                    for (int j = start[1]; j <= end[1]; j++)
                    {
                        switch (action)
                        {
                            case "turn on":
                                grid[(i, j)] += 1;
                                break;

                            case "turn off":
                                grid[(i, j)] -= 1;
                                break;

                            case "toggle":
                                grid[(i, j)] += 2;
                                break;
                        }

                        if (grid[(i, j)] < 0)
                        {
                            grid[(i, j)] = 0;
                        }
                    }
                }
            }

            result = grid.Sum(x => x.Value);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}
