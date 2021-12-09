using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day5
    {
        private readonly string realData = "ojvtpuvg";

        [TestCase("abc", "18f47a30", Explicit = true)]
        [TestCase(null, "4543c154", Explicit = true)]
        public void Part1(string input, string expected)
        {
            input = input ?? realData;
            var result = "";

            var md5 = MD5.Create();
            for (int i = 0; i <= int.MaxValue; i++)
            {
                var computedHash = md5.ComputeHash(Encoding.ASCII.GetBytes(string.Format("{0}{1}", input, i)));
                var hexString = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
                if (hexString.StartsWith("00000"))
                {
                    result += hexString[5];
                    if (result.Length == 8)
                    {
                        break;
                    }
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("abc", "05ace8e3", Explicit = true)]
        [TestCase(null, "1050cbbd", Explicit = true)]
        public void Part2(string input, string expected)
        {
            input = input ?? realData;
            var password = new string[8];
            var md5 = MD5.Create();
            for (int i = 0; i <= int.MaxValue; i++)
            {
                var computedHash = md5.ComputeHash(Encoding.ASCII.GetBytes(string.Format("{0}{1}", input, i)));
                var hexString = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
                if (hexString.StartsWith("00000"))
                {
                    var position = hexString[5];
                    var character = hexString[6];
                    int positionNumber;

                    if (int.TryParse(position.ToString(), out positionNumber) && positionNumber < password.Length)
                    {
                        if (string.IsNullOrWhiteSpace(password[positionNumber]))
                        {
                            password[positionNumber] = character.ToString();
                        }
                    }

                    if (password.All(p => !string.IsNullOrWhiteSpace(p)))
                    {
                        break;
                    }
                }
            }

            var result = string.Join("", password);

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            Console.WriteLine(result);
        }
    }
}
