using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day3
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day3.dat"));

        [TestCase("1,2,2", 1)]
        [TestCase("1,1,3", 0)]
        [TestCase("1,2,2\n1,1,3", 1)]
        [TestCase("1,2,2\n4,1,1", 1)]
        [TestCase(null, 1050)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;

            var tests = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var result = 0;

            foreach (var test in tests)
            {
                var testParts = test.Split(',').Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => int.Parse(t.Trim())).ToArray();
                if (((testParts[1] + testParts[2]) > testParts[0]) && ((testParts[0] + testParts[2]) > testParts[1]) && ((testParts[0] + testParts[1]) > testParts[2]))
                {
                    result++;
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("1,1\n2,1\n2,3", 1)]
        [TestCase("1,4\n2,1\n2,1", 1)]
        [TestCase("102,302,502\n101,301,501\n103,303,503\n201,401,601\n202,402,602\n203,403,603", 6)]
        [TestCase(null, 1921)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            var lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var values = lines.Select(x => x.Split(',').Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => int.Parse(t.Trim())).ToArray()).ToArray();

            for (int i = 0; i < values.Length; i += 3)
            {
                var line1 = values[i];
                var line2 = values[i + 1];
                var line3 = values[i + 2];

                for (int k = 0; k < line1.Length; k++)
                {
                    if (((line2[k] + line3[k]) > line1[k]) && ((line1[k] + line3[k]) > line2[k]) && ((line1[k] + line2[k]) > line3[k]))
                    {
                        result++;
                    }
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
