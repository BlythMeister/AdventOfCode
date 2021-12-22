using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day1
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day1.dat"));

        [TestCase("R2", 2)]
        [TestCase("R2, L1", 3)]
        [TestCase("R2, L1, L1", 2)]
        [TestCase(null, 353)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;

            var position = new Tuple<int, int>(0, 0);
            var movements = input.Split(',').Select(i => i.Trim()).ToList();
            var headings = new List<string> { "N", "E", "S", "W" };
            var currentHeading = 0;

            foreach (var movement in movements)
            {
                var direction = movement[..1];
                var amount = int.Parse(movement[1..]);

                if (direction == "R")
                {
                    currentHeading++;
                    if (currentHeading > 3) currentHeading = 0;
                }
                else if (direction == "L")
                {
                    currentHeading--;
                    if (currentHeading < 0) currentHeading = 3;
                }

                switch (headings[currentHeading])
                {
                    case "N":
                        position = new Tuple<int, int>(position.Item1 + amount, position.Item2);
                        break;
                    case "E":
                        position = new Tuple<int, int>(position.Item1, position.Item2 + amount);
                        break;
                    case "S":
                        position = new Tuple<int, int>(position.Item1 - amount, position.Item2);
                        break;
                    case "W":
                        position = new Tuple<int, int>(position.Item1, position.Item2 - amount);
                        break;
                }
            }

            var result = Math.Abs(position.Item1) + Math.Abs(position.Item2);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("R8, R4, R4, R8", 4)]
        [TestCase(null, 152)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            var position = new Tuple<int, int>(0, 0);
            var movements = input.Split(',').Select(i => i.Trim()).ToList();
            var headings = new List<string> { "N", "E", "S", "W" };
            var currentHeading = 0;

            var visitedLocations = new List<Tuple<int, int>>();

            foreach (var movement in movements)
            {
                if (result > 0) break;

                var direction = movement[..1];
                var amount = int.Parse(movement[1..]);

                if (direction == "R")
                {
                    currentHeading++;
                    if (currentHeading > 3) currentHeading = 0;
                }
                else if (direction == "L")
                {
                    currentHeading--;
                    if (currentHeading < 0) currentHeading = 3;
                }

                for (int i = 1; i <= amount; i++)
                {
                    switch (headings[currentHeading])
                    {
                        case "N":
                            position = new Tuple<int, int>(position.Item1 + 1, position.Item2);
                            break;
                        case "E":
                            position = new Tuple<int, int>(position.Item1, position.Item2 + 1);
                            break;
                        case "S":
                            position = new Tuple<int, int>(position.Item1 - 1, position.Item2);
                            break;
                        case "W":
                            position = new Tuple<int, int>(position.Item1, position.Item2 - 1);
                            break;
                    }

                    if (visitedLocations.Any(l => l.Item1 == position.Item1 && l.Item2 == position.Item2))
                    {
                        result = Math.Abs(position.Item1) + Math.Abs(position.Item2);
                        break;
                    }
                    else
                    {
                        visitedLocations.Add(position);
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
