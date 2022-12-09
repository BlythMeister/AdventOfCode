using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2022
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day02
    {
        [TestCase("Day02-Sample", 15)]
        [TestCase("Day02", 8933)]
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

        [TestCase("Day02-Sample", 12)]
        [TestCase("Day02", 11998)]
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

        private static long Execute1(string[] inputs)
        {
            var score = 0;
            foreach (var input in inputs)
            {
                var inputParts = input.Split(' ');
                var oppo = inputParts[0] switch
                {
                    "A" => Selection.Rock,
                    "B" => Selection.Paper,
                    "C" => Selection.Scissors
                };

                var you = inputParts[1] switch
                {
                    "X" => Selection.Rock,
                    "Y" => Selection.Paper,
                    "Z" => Selection.Scissors
                };

                var result = (oppo, you) switch
                {
                    (Selection.Rock, Selection.Rock) => Result.Draw,
                    (Selection.Paper, Selection.Paper) => Result.Draw,
                    (Selection.Scissors, Selection.Scissors) => Result.Draw,
                    (Selection.Rock, Selection.Paper) => Result.Win,
                    (Selection.Paper, Selection.Scissors) => Result.Win,
                    (Selection.Scissors, Selection.Rock) => Result.Win,
                    _ => Result.Lose
                };

                score += (int)you;

                score += (int)result;
            }

            return score;
        }

        private static long Execute2(string[] inputs)
        {
            var score = 0;
            foreach (var input in inputs)
            {
                var inputParts = input.Split(' ');
                var oppo = inputParts[0] switch
                {
                    "A" => Selection.Rock,
                    "B" => Selection.Paper,
                    "C" => Selection.Scissors
                };

                var result = inputParts[1] switch
                {
                    "X" => Result.Lose,
                    "Y" => Result.Draw,
                    "Z" => Result.Win
                };

                var you = (oppo, result) switch
                {
                    (Selection.Rock, Result.Lose) => Selection.Scissors,
                    (Selection.Rock, Result.Win) => Selection.Paper,
                    (Selection.Rock, Result.Draw) => Selection.Rock,
                    (Selection.Paper, Result.Lose) => Selection.Rock,
                    (Selection.Paper, Result.Win) => Selection.Scissors,
                    (Selection.Paper, Result.Draw) => Selection.Paper,
                    (Selection.Scissors, Result.Lose) => Selection.Paper,
                    (Selection.Scissors, Result.Win) => Selection.Rock,
                    (Selection.Scissors, Result.Draw) => Selection.Scissors,
                };

                score += (int)you;
                score += (int)result;
            }

            return score;
        }

        private enum Selection
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        private enum Result
        {
            Win = 6,
            Draw = 3,
            Lose = 0
        }
    }
}
