using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day3
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day3.dat"));

        [TestCase("..##.......\n#...#...#..\n.#....#..#.\n..#.#...#.#\n.#...##..#.\n..#.##.....\n.#.#.#....#\n.#........#\n#.##...#...\n#...##....#\n.#..#...#.#", 7)]
        [TestCase(null, 173)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? input.Split('\n') : realData;
            var result = Execute(lines, new List<(int x, int y)> { (3, 1) });

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("..##.......\n#...#...#..\n.#....#..#.\n..#.#...#.#\n.#...##..#.\n..#.##.....\n.#.#.#....#\n.#........#\n#.##...#...\n#...##....#\n.#..#...#.#", 336)]
        [TestCase(null, 173)]
        public void Part2(string input, int? expected)
        {
            var lines = input != null ? input.Split('\n') : realData;
            var result = Execute(lines, new List<(int x, int y)> { (1,1),(3, 1),(5,1),(7,1),(1,2) });

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private long Execute(string[] inputs, List<(int x, int y)> movements)
        {
            var map = new List<List<char>>();

            void AddMoreToMap()
            {
                if (!map.Any())
                {
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        map.Add(new List<char>());
                    }
                }

                for (int i = 0; i < inputs.Length; i++)
                {
                    map[i].AddRange(inputs[i]);
                }
            }

            long totalTrees = 0;
            AddMoreToMap();

            foreach (var movement in movements)
            {
                (int x, int y) position = (0, 0);
                var trees = 0;
                while (position.y < inputs.Length)
                {
                    if (map[position.y].Count <= position.x)
                    {
                        AddMoreToMap();
                    }

                    if (map[position.y][position.x] == '#')
                    {
                        trees++;
                    }

                    position.x += movement.x;
                    position.y += movement.y;
                }

                if (totalTrees > 0)
                {
                    totalTrees *= trees;
                }
                else
                {
                    totalTrees = trees;
                }
            }

            return totalTrees;
        }
    }
}
