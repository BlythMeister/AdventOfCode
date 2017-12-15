using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day4
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day4.dat"));

        [TestCase("aaaaa-bbb-z-y-x-123[abxyz]", 123)]
        [TestCase("a-b-c-d-e-f-g-h-987[abcde]", 987)]
        [TestCase("not-a-real-room-404[oarel]", 404)]
        [TestCase("totally-real-room-200[decoy]", 0)]
        [TestCase("totally-real-room-200[decoy]\nnot-a-real-room-404[oarel]", 404)]
        [TestCase(null, 158835)]
        public void Part1(string input, int? expected)
        {
            input = input ?? realData;
            var result = 0;

            var tests = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => i.Trim());

            foreach (var test in tests)
            {
                var name = test.Substring(0, test.LastIndexOf("-"));
                var sectorAndChecksum = test.Replace(name, "").Substring(1);
                var sector = sectorAndChecksum.Substring(0, sectorAndChecksum.IndexOf("["));
                var checksum = sectorAndChecksum.Replace(sector, "").Replace("[", "").Replace("]", "");

                var letterCounts = new Dictionary<string, int>();

                foreach (var character in name.Replace("-", ""))
                {
                    var letter = character.ToString();
                    if (letterCounts.ContainsKey(letter))
                    {
                        letterCounts[letter]++;
                    }
                    else
                    {
                        letterCounts.Add(letter, 1);
                    }
                }

                var orderedletterCounts = letterCounts.OrderByDescending(c => c.Value).ThenBy(c => c.Key);

                var first5Keys = orderedletterCounts.Take(5).Select(c => c.Key);
                var calculatedChecksum = String.Join("", first5Keys);

                if (checksum == calculatedChecksum)
                {
                    result = result + int.Parse(sector);
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("very encrypted name", "qzmt-zixmtkozy-ivhz-343[]", "343")]
        [TestCase("northpole object storage", null, "993", Explicit = true)]
        public void Part2(string searchRoom, string input, string expected)
        {
            input = input ?? realData;
            var result = "";

            var tests = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => i.Trim());
            var alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            foreach (var test in tests)
            {
                var name = test.Substring(0, test.LastIndexOf("-"));
                var sectorAndChecksum = test.Replace(name, "").Substring(1);
                var sector = sectorAndChecksum.Substring(0, sectorAndChecksum.IndexOf("["));

                var decryptedName = string.Empty;

                foreach (var character in name.Replace("-", " "))
                {
                    var letter = character.ToString();
                    if (letter == " ")
                    {
                        decryptedName = decryptedName + letter;
                    }
                    else
                    {
                        var letterPos = 0;
                        for (int i = 0; i < alphabet.Length; i++)
                        {
                            if (alphabet[i] == letter)
                            {
                                letterPos = i;
                                break;
                            }
                        }

                        for (int i = 0; i < int.Parse(sector); i++)
                        {
                            letterPos = letterPos + 1;
                            if (letterPos >= alphabet.Length)
                            {
                                letterPos = 0;
                            }
                        }

                        decryptedName = decryptedName + alphabet[letterPos];
                    }
                }

                if (decryptedName == searchRoom)
                {
                    result = sector;
                    break;
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            Console.WriteLine(result);
        }
    }
}
