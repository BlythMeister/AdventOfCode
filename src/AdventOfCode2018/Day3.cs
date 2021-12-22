using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2018
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day3
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day3.dat"));

        [TestCase("#1 @ 1,3: 4x4|#2 @ 3,1: 4x4|#3 @ 5,5: 2x2", 4)]
        [TestCase(null, 113966)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? input.Split('|') : realData;
            var result = 0;

            var area = new int[2000, 2000];

            foreach (var line in lines)
            {
                var indexOfAt = line.IndexOf("@");
                var indexOfColon = line.IndexOf(":");

                var coords = line.Substring(indexOfAt + 1, indexOfColon - indexOfAt - 1).Trim().Split(',');
                var size = line[(indexOfColon + 1)..].Trim().Split('x');

                var posX = int.Parse(coords[0]);
                var posY = int.Parse(coords[1]);

                var sizeX = int.Parse(size[0]);
                var sizeY = int.Parse(size[1]);

                for (int x = posX; x < posX + sizeX; x++)
                {
                    for (int y = posY; y < posY + sizeY; y++)
                    {
                        area[x, y]++;
                    }
                }
            }

            for (int i = 0; i < 2000; i++)
            {
                for (int j = 0; j < 2000; j++)
                {
                    if (area[i, j] > 1) result++;
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
