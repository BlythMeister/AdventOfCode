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
            input ??= realData;
            var result = "";

            var (tree, _) = GetTreeAndWeights(input);

            foreach (var treeItem in tree)
            {
                if (!tree.Any(x => x.Value.Contains(treeItem.Key)))
                {
                    result = treeItem.Key;
                    break;
                }
            }

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            Console.WriteLine(result);
        }

        [TestCase("pbga (66)\nxhth (57)\nebii (61)\nhavc (66)\nktlj (57)\nfwft (72) -> ktlj, cntj, xhth\nqoyq (66)\npadx (45) -> pbga, havc, qoyq\ntknk (41) -> ugml, padx, fwft\njptl (61)\nugml (68) -> gyxo, ebii, jptl\ngyxo (61)\ncntj (57)", 60)]
        [TestCase(null, 2310)]
        public void Part2(string input, int? expected)
        {
            input ??= realData;
            var result = 0;

            var (tree, weights) = GetTreeAndWeights(input);

            var treebase = "";

            foreach (var treeItem in tree)
            {
                if (!tree.Any(x => x.Value.Contains(treeItem.Key)))
                {
                    treebase = treeItem.Key;
                    break;
                }
            }

            var invalidLeaves = new Dictionary<string, int>();

            int SumAllLeaves(string leaf)
            {
                var childSums = new Dictionary<string, int>();
                if (tree.ContainsKey(leaf))
                {
                    foreach (var subLeaf in tree[leaf])
                    {
                        childSums.Add(subLeaf, SumAllLeaves(subLeaf));
                    }

                    var childGroups = childSums.GroupBy(x => x.Value);
                    if (childGroups.Count() > 1)
                    {
                        var invalidLeaf = "";
                        var invaildtotal = 0;
                        var validtotal = 0;
                        foreach (var childGroup in childGroups)
                        {
                            if (childGroup.Count() == 1)
                            {
                                invalidLeaf = childGroup.First().Key;
                                invaildtotal = childGroup.First().Value;
                            }
                            else
                            {
                                validtotal = childGroup.First().Value;
                            }
                        }

                        var invalidLeafValue = weights[invalidLeaf];
                        var difference = invaildtotal - validtotal;
                        invalidLeaves.Add(invalidLeaf, invalidLeafValue - difference);
                    }
                }

                return weights[leaf] + childSums.Sum(x => x.Value);
            }

            SumAllLeaves(treebase);

            result = invalidLeaves.FirstOrDefault().Value;

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }

            Console.WriteLine(result);
        }

        private static (Dictionary<string, List<string>> tree, Dictionary<string, int> weights) GetTreeAndWeights(string input)
        {
            var tree = new Dictionary<string, List<string>>();
            var weights = new Dictionary<string, int>();

            foreach (var line in input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                var item = line[..line.IndexOf(" ")];

                if (line.Contains("->"))
                {
                    var children = line[(line.IndexOf("->") + 3)..].Split(',').Select(x => x.Trim());

                    tree.Add(item, children.ToList());
                }

                if (line.Contains('(') && line.Contains(')'))
                {
                    var openChar = line.IndexOf("(") + 1;
                    var closeChar = line.IndexOf(")");
                    var length = closeChar - openChar;
                    var weight = int.Parse(line.Substring(openChar, length));
                    weights.Add(item, weight);
                }
            }

            return (tree, weights);
        }
    }
}
