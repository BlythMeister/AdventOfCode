using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day07
    {
        [TestCase("Day07-Sample", 37)]
        [TestCase("Day07", 354129)]
        public void Part1(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"))[0].Split(",");
            var result = Execute(lines, false);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("Day07-Sample", 168)]
        [TestCase("Day07", 98905973)]
        public void Part2(string inputFile, long? expected)
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", $"{inputFile}.dat"))[0].Split(",");
            var result = Execute(lines, true);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static long Execute(string[] inputs, bool incrementingCost)
        {
            var positions = inputs.Select(int.Parse).ToArray();
            var fuelCost = long.MaxValue;

            for (int i = positions.Min(); i <= positions.Max(); i++)
            {
                long positionCost = 0;
                foreach (var position in positions)
                {
                    var amountToMove = Math.Abs(position - i);
                    if (incrementingCost)
                    {
                        for (int j = 1; j <= amountToMove; j++)
                        {
                            positionCost += j;
                        }
                    }
                    else
                    {
                        positionCost += amountToMove;
                    }
                }

                if (fuelCost > positionCost)
                {
                    fuelCost = positionCost;
                }
            }

            return fuelCost;
        }
    }
}
