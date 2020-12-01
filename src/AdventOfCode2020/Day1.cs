using NUnit.Framework;
using System;
using System.IO;

namespace AdventOfCode2020
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day1
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day1.dat"));

        [TestCase("1721,979,366,299,675,1456", 514579)]
        [TestCase(null, 633216)]
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

        [TestCase("1721,979,366,299,675,1456", 241861950)]
        [TestCase(null, 68348924)]
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

        private int Execute(string[] inputs, bool compare3)
        {
            for (var i = 0; i < inputs.Length; i++)
            {
                var firstNo = int.Parse(inputs[i]);
                for (var j = 0; j < inputs.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var secondNo = int.Parse(inputs[j]);

                    if (compare3)
                    {
                        for (var k = 0; k < inputs.Length; k++)
                        {
                            if (i == k || j == k)
                            {
                                continue;
                            }

                            var thirdNo = int.Parse(inputs[k]);

                            if (firstNo + secondNo + thirdNo == 2020)
                            {
                                return firstNo * secondNo * thirdNo;
                            }
                        }
                    }
                    else
                    {
                        if (firstNo + secondNo == 2020)
                        {
                            return firstNo * secondNo;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
