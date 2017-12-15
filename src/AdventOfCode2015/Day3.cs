using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2015
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day3
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day3.dat"));

        [TestCase(">", 2)]
        [TestCase("^>v<", 4)]
        [TestCase("^v^v^v^v^v", 2)]
        [TestCase(null, 2592)]
        public void Part1(string input, int? expected)
        {
            input = input ?? realData;
            var xPosition = 0;
            var yPosition = 0;

            var visited = new HashSet<(int, int)> { (xPosition, yPosition) };

            foreach (var movement in input)
            {
                switch (movement)
                {
                    case '>':
                        yPosition++;
                        break;
                    case '<':
                        yPosition--;
                        break;
                    case '^':
                        xPosition++;
                        break;
                    case 'v':
                        xPosition--;
                        break;
                }

                visited.Add((xPosition, yPosition));
            }

            var result = visited.Count;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("^v", 3)]
        [TestCase("^>v<", 3)]
        [TestCase("^v^v^v^v^v", 11)]
        [TestCase(null, 2360)]
        public void Part2(string input, int? expected)
        {
            input = input ?? realData;
            var xPositionSanta = 0;
            var yPositionSanta = 0;

            var xPositionRoboSanta = 0;
            var yPositionRoboSanta = 0;

            var santaMove = true;

            var visited = new HashSet<(int, int)> { (0, 0) };

            foreach (var movement in input)
            {
                switch (movement)
                {
                    case '>':
                        if (santaMove)
                        {
                            yPositionSanta++;
                        }
                        else
                        {
                            yPositionRoboSanta++;
                        }
                        break;
                    case '<':
                        if (santaMove)
                        {
                            yPositionSanta--;
                        }
                        else
                        {
                            yPositionRoboSanta--;
                        }
                        break;
                    case '^':
                        if (santaMove)
                        {
                            xPositionSanta++;
                        }
                        else
                        {
                            xPositionRoboSanta++;
                        }
                        break;
                    case 'v':
                        if (santaMove)
                        {
                            xPositionSanta--;
                        }
                        else
                        {
                            xPositionRoboSanta--;
                        }
                        break;
                }

                if (santaMove)
                {
                    visited.Add((xPositionSanta, yPositionSanta));
                }
                else
                {
                    visited.Add((xPositionRoboSanta, yPositionRoboSanta));
                }

                santaMove = !santaMove;

            }

            var result = visited.Count;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }
    }
}
