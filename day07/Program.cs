using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day07
{
    class Link
    {
        public string Parent { get; set; }
        public string Child { get; set; }
        public int Count { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var links = new List<Link>();
            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, @"[,\s]*([0-9]*)(.*?)bags?( contain)?");
                for (int i = 1; i < matches.Count; i++)
                {
                    if (int.TryParse(matches[i].Groups[1].Value.Trim(), out var bags))
                    {
                        links.Add(new Link
                        {
                            Parent = matches[0].Groups[2].Value.Trim(),
                            Child = matches[i].Groups[2].Value.Trim(),
                            Count = bags
                        });
                    }
                }
            }

            var uniques = new HashSet<string>();
            Recurse(links, "shiny gold", uniques);
            Console.WriteLine($"Part 1 {uniques.Count}");

            var count = Recurse2(links, "shiny gold");
            Console.WriteLine($"Part 2 {count - 1}"); // -1 no need to count "shiny gold" itself.
        }

        private static void Recurse(List<Link> links, string node, HashSet<string> uniques)
        {
            foreach (var link in links.Where(l => l.Child == node))
            {
                uniques.Add(link.Parent);
                Recurse(links, link.Parent, uniques);
            }
        }

        private static int Recurse2(List<Link> links, string node)
        {
            var count = 1;

            foreach (var link in links.Where(l => l.Parent == node))
            {
                count += link.Count * Recurse2(links, link.Child);
            }

            return count;
        }
    }
}
