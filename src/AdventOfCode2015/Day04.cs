using NUnit.Framework;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2015
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day04
    {
        private readonly string realData = "yzbqklnj";

        [TestCase("abcdef", 609043, Explicit = true)]
        [TestCase("pqrstuv", 1048970, Explicit = true)]
        [TestCase(null, 282749, Explicit = true)]
        public void Part1(string input, int? expected)
        {
            input ??= realData;
            var result = GetHashWithLeadingZeros(input, "00000");

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase(null, 9962624, Explicit = true)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = GetHashWithLeadingZeros(input, "000000");

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        private static int GetHashWithLeadingZeros(string input, string leadingText)
        {
            var result = 0;

            var md5Provider = MD5.Create();

            while (true)
            {
                var toHash = new UTF8Encoding().GetBytes($"{input}{result}");
                var computed = md5Provider.ComputeHash(toHash);
                var hashed = new StringBuilder();
                foreach (var t in computed)
                {
                    hashed.Append(t.ToString("x2"));
                }

                if (hashed.ToString().StartsWith(leadingText))
                {
                    break;
                }
                result++;
            }
            return result;
        }
    }
}
