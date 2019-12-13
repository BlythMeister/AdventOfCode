using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2019
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day1
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day1.dat"));

        [TestCase("12", 2)]
        [TestCase("14", 2)]
        [TestCase("1969", 654)]
        [TestCase("100756", 33583)]
        [TestCase(null, 3334297)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? input.Split(',') : realData;
            var result = Execute(lines, false);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("14", 2)]
        [TestCase("1969", 966)]
        [TestCase("100756", 50346)]
        [TestCase(null, 4998565)]
        public void Part2(string input, int? expected)
        {
            var lines = input != null ? input.Split(',') : realData;
            var result = Execute(lines, true);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private int Execute(string[] inputs, bool fuelNeedsFuel)
        {
            var result = 0;
            foreach (var input in inputs)
            {
                var inputNumber = decimal.Parse(input);
                var devided = (int)Math.Floor(inputNumber / 3);
                var fuel = devided - 2;
                result += fuel;

                if (fuelNeedsFuel)
                {
                    while (true)
                    {
                        devided = (int)Math.Floor(fuel / (decimal)3);
                        fuel = devided - 2;
                        if (fuel > 0)
                        {
                            result += fuel;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
