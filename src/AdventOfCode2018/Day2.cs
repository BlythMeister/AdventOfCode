using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day2
    {
        private readonly string[] realData = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day2.dat"));

        [TestCase("abcdef,bababc,abbcde,abcccd,aabcdd,abcdee,ababab", 12)]
        [TestCase(null, 4920)]
        public void Part1(string input, int? expected)
        {
            var lines = input != null ? input.Split(',') : realData;

            var twoLetter = 0;
            var threeLetter = 0;

            foreach (var s in lines)
            {
                var letters = s.GroupBy(x => x);
                if (letters.Any(x => x.Count() == 2)) twoLetter++;
                if (letters.Any(x => x.Count() == 3)) threeLetter++;
            }

            var result = twoLetter * threeLetter;

            if (expected != null)
            {
                Assert.AreEqual(expected.Value, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("abcde,fghij,klmno,pqrst,fguij,axcye,wvxyz", "fgij")]
        [TestCase(null, "fonbwmjquwtapeyzikghtvdxl")]
        public void Part2(string input, string expected)
        {
            var lines = input != null ? input.Split(',') : realData;
            var result = string.Empty;
            for (int i = 0; i < lines.Length; i++)
            {
                var line1 = lines[i];
                for (int j = 0; j < lines.Length; j++)
                {
                    if (i == j) continue;
                    var line2 = lines[j];
                    var differentCharacters = 0;
                    for (int k = 0; k < line1.Length; k++)
                    {
                        if (line1[k] != line2[k])
                        {
                            differentCharacters++;
                        }
                        else
                        {
                            result += line1[k];
                        }
                    }

                    if (differentCharacters == 1)
                    {
                        break;
                    }

                    result = string.Empty;
                }

                if (!string.IsNullOrWhiteSpace(result))
                {
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
