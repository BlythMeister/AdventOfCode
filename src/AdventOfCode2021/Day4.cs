using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day4
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day4.dat"));

        [TestCase("Day4-Sample.dat", 4512)]
        [TestCase(null, 31424)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", input)) : realData;
            var result = ExecutePart1(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day4-Sample.dat", 1924)]
        [TestCase(null, 23042)]
        public void Part2(string input, int? expected)
        {
            var lines = input != null ? File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", input)) : realData;
            var result = ExecutePart2(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private int ExecutePart1(string[] inputs)
        {
            var numbers = inputs[0].Split(',').Select(int.Parse);

            var boards = BuildBoards(inputs);

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[i, j] == number)
                            {
                                board[i, j] = number + 1000;
                            }
                        }
                    }

                    if (IsBoardWinner(board))
                    {
                        return SumUnmarked(board, number);
                    }
                }
            }

            return 0;
        }

        private int ExecutePart2(string[] inputs)
        {
            var numbers = inputs[0].Split(',').Select(int.Parse);

            var boards = BuildBoards(inputs);

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[i, j] == number)
                            {
                                board[i, j] = number + 1000;
                            }
                        }
                    }
                }

                if (boards.Count == 1)
                {
                    if (IsBoardWinner(boards[0]))
                    {
                        return SumUnmarked(boards[0], number);
                    }
                }
                boards.RemoveAll(IsBoardWinner);
            }

            return 0;
        }

        private static int SumUnmarked(int[,] board, int number)
        {
            var sumUnmarked = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board[i, j] < 1000)
                    {
                        sumUnmarked += board[i, j];
                    }
                }
            }

            return sumUnmarked * number;
        }

        private static bool IsBoardWinner(int[,] board)
        {
            var winner = false;
            for (int i = 0; i < 5; i++)
            {
                var winnerRow = true;
                for (int j = 0; j < 5; j++)
                {
                    if (board[i, j] < 1000)
                    {
                        winnerRow = false;
                        break;
                    }
                }

                if (winnerRow)
                {
                    winner = true;
                    break;
                }
            }

            if (!winner)
            {
                for (int i = 0; i < 5; i++)
                {
                    var winnerColumn = true;
                    for (int j = 0; j < 5; j++)
                    {
                        if (board[j, i] < 1000)
                        {
                            winnerColumn = false;
                            break;
                        }
                    }

                    if (winnerColumn)
                    {
                        winner = true;
                        break;
                    }
                }
            }

            return winner;
        }

        private static List<int[,]> BuildBoards(string[] inputs)
        {
            var boards = new List<int[,]>();

            int[,] boardBuilder = null;
            var boardLine = 0;
            for (int i = 1; i < inputs.Length; i++)
            {
                var line = inputs[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (boardBuilder != null)
                    {
                        boards.Add(boardBuilder);
                    }

                    boardBuilder = new int[5, 5];
                    boardLine = 0;
                }
                else
                {
                    var lineParts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < lineParts.Length; j++)
                    {
                        boardBuilder[boardLine, j] = int.Parse(lineParts[j]);
                    }

                    boardLine++;
                }
            }

            boards.Add(boardBuilder);
            return boards;
        }
    }
}
