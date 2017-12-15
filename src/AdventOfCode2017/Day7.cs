using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class Day7
    {
        private readonly string realData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Day7.dat"));

        [TestCase("pbga (66)\nxhth (57)\nebii (61)\nhavc (66)\nktlj (57)\nfwft (72) -> ktlj, cntj, xhth\nqoyq (66)\npadx (45) -> pbga, havc, qoyq\ntknk (41) -> ugml, padx, fwft\njptl (61)\nugml (68) -> gyxo, ebii, jptl\ngyxo (61)\ncntj (57)", "tknk")]
        [TestCase(null, "cqmvs")]
        public void Part1(string input, string expected)
        {
            input = input ?? realData;
            var result = "";
            var itemList = new Dictionary<string, string>();

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                if (line.Contains("->"))
                {
                    var item = line.Substring(0, line.IndexOf(" "));
                    var children = line.Substring(line.IndexOf("->") + 3).Split(',').Select(x => x.Trim());
                    foreach (var child in children)
                    {
                        itemList.Add(child, item);
                    }
                }
            }

            foreach (var item in itemList)
            {
                if (!itemList.ContainsKey(item.Value))
                {
                    result = item.Value;
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