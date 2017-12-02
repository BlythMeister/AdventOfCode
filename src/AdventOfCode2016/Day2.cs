using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2016
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day2
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day2.dat"));

        [TestCase("ULL\nRRDDD\nLURDL\nUUUUD", "1985")]
        [TestCase(null, "14894")]
        public void Part1(string input, string expected)
        {
            input = input ?? realData;

            var position = new Tuple<int, int>(1, 1);
            var buttonMovements = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var buttons = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "4", "5", "6" }, new List<string> { "7", "8", "9" } };
            var result = "";
            foreach (var buttonMovement in buttonMovements)
            {
                foreach (var movement in buttonMovement)
                {
                    int newItem;
                    switch (movement.ToString())
                    {
                        case "U":
                            newItem = position.Item1 - 1;
                            if (newItem < 0) newItem = 0;
                            position = new Tuple<int, int>(newItem, position.Item2);
                            break;
                        case "D":
                            newItem = position.Item1 + 1;
                            if (newItem > 2) newItem = 2;
                            position = new Tuple<int, int>(newItem, position.Item2);
                            break;
                        case "R":
                            newItem = position.Item2 + 1;
                            if (newItem > 2) newItem = 2;
                            position = new Tuple<int, int>(position.Item1, newItem);
                            break;
                        case "L":
                            newItem = position.Item2 - 1;
                            if (newItem < 0) newItem = 0;
                            position = new Tuple<int, int>(position.Item1, newItem);
                            break;
                    }
                }

                result += buttons[position.Item1][position.Item2];
            }

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("ULL\nRRDDD\nLURDL\nUUUUD", "5DB3")]
        [TestCase(null, "26B96")]
        public void Part2(string input, string expected)
        {
            input = input ?? realData;

            var position = new Tuple<int, int>(2, 0);
            var buttonMovements = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var buttons = new List<List<string>> { new List<string> { "", "", "1", "", "" }, new List<string> { "", "2", "3", "4", "" }, new List<string> { "5", "6", "7", "8", "9" }, new List<string> { "", "A", "B", "C", "" }, new List<string> { "", "", "D", "", "" } };
            var result = "";
            foreach (var buttonMovement in buttonMovements)
            {
                foreach (var movement in buttonMovement)
                {
                    Tuple<int, int> potentialNewPosition;
                    int newItem;
                    switch (movement.ToString())
                    {
                        case "U":
                            newItem = position.Item1 - 1;
                            if (newItem < 0) newItem = 0;
                            potentialNewPosition = new Tuple<int, int>(newItem, position.Item2);
                            if (!string.IsNullOrWhiteSpace(buttons[potentialNewPosition.Item1][potentialNewPosition.Item2]))
                            {
                                position = potentialNewPosition;
                            }
                            break;
                        case "D":
                            newItem = position.Item1 + 1;
                            if (newItem > 4) newItem = 4;
                            potentialNewPosition = new Tuple<int, int>(newItem, position.Item2);
                            if (!string.IsNullOrWhiteSpace(buttons[potentialNewPosition.Item1][potentialNewPosition.Item2]))
                            {
                                position = potentialNewPosition;
                            }
                            break;
                        case "R":
                            newItem = position.Item2 + 1;
                            if (newItem > 4) newItem = 4;
                            potentialNewPosition = new Tuple<int, int>(position.Item1, newItem);
                            if (!string.IsNullOrWhiteSpace(buttons[potentialNewPosition.Item1][potentialNewPosition.Item2]))
                            {
                                position = potentialNewPosition;
                            }
                            break;
                        case "L":
                            newItem = position.Item2 - 1;
                            if (newItem < 0) newItem = 0;
                            potentialNewPosition = new Tuple<int, int>(position.Item1, newItem);
                            if (!string.IsNullOrWhiteSpace(buttons[potentialNewPosition.Item1][potentialNewPosition.Item2]))
                            {
                                position = potentialNewPosition;
                            }
                            break;
                    }
                }

                result += buttons[position.Item1][position.Item2];
            }

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            Console.WriteLine(result);
        }
    }
}
