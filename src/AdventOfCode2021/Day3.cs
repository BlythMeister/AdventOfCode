using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day3
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day3.dat"));

        [TestCase("00100,11110,10110,10111,10101,01111,00111,11100,10000,11001,00010,01010", 198)]
        [TestCase(null, 4160394)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? input.Split(',') : realData;
            var result = ExecutePart1(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("00100,11110,10110,10111,10101,01111,00111,11100,10000,11001,00010,01010", 230)]
        [TestCase(null, 4125600)]
        public void Part2(string input, int? expected)
        {
            var lines = input != null ? input.Split(',') : realData;
            var result = ExecutePart2(lines);

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static int ExecutePart1(string[] inputs)
        {
            var gammaRate = "";
            var epsilonRate = "";

            for (var i = 0; i < inputs[0].Length; i++)
            {
                var result = MoreOrLessZerosAtPosition(inputs, i);

                if (result is Result.MoreZero or Result.Equal)
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
                else
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
            }

            var gammaRateNumber = Convert.ToInt32(gammaRate, 2);
            var epsilonRateNumber = Convert.ToInt32(epsilonRate, 2);

            return gammaRateNumber * epsilonRateNumber;
        }

        private static int ExecutePart2(string[] inputs)
        {
            var oxygen = "";
            var c02 = "";

            for (var i = 0; i < inputs[0].Length; i++)
            {
                var filteredInput = inputs.Where(x => oxygen == "" || x.StartsWith(oxygen)).ToArray();
                if (filteredInput.Length == 1)
                {
                    oxygen = filteredInput[0];
                    break;
                }
                var result = MoreOrLessZerosAtPosition(filteredInput, i);
                if (result == Result.MoreZero)
                {
                    oxygen += "0";
                }
                else
                {
                    oxygen += "1";
                }
            }

            for (var i = 0; i < inputs[0].Length; i++)
            {
                var filteredInput = inputs.Where(x => c02 == "" || x.StartsWith(c02)).ToArray();
                if (filteredInput.Length == 1)
                {
                    c02 = filteredInput[0];
                    break;
                }
                var result = MoreOrLessZerosAtPosition(filteredInput, i);
                if (result == Result.MoreZero)
                {
                    c02 += "1";
                }
                else
                {
                    c02 += "0";
                }
            }

            var oxygenNumber = Convert.ToInt32(oxygen, 2);
            var c02Number = Convert.ToInt32(c02, 2);

            return oxygenNumber * c02Number;
        }

        private static Result MoreOrLessZerosAtPosition(string[] inputs, int i)
        {
            var zeroCount = 0;
            var oneCount = 0;
            foreach (var input in inputs)
            {
                if (input[i] == '0')
                {
                    zeroCount++;
                }
                else
                {
                    oneCount++;
                }
            }

            if (zeroCount == oneCount)
            {
                return Result.Equal;
            }

            if (zeroCount > oneCount)
            {
                return Result.MoreZero;
            }

            return Result.LessZero;
        }

        private enum Result
        {
            MoreZero,
            Equal,
            LessZero
        }
    }
}
