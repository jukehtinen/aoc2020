using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day16
{
    class Rule
    {
        public string Field { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
        public bool IsValid(int val) => Min <= val && Max >= val || Min2 <= val && Max2 >= val;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var rules = new List<Rule>();
            foreach (var l in input)
            {
                if (string.IsNullOrEmpty(l)) break;
                var groups = Regex.Match(l, @"(\D+): (\d+)-(\d+) or (\d+)-(\d+)").Groups;
                rules.Add(new Rule
                {
                    Field = groups[1].Value,
                    Min = int.Parse(groups[2].Value),
                    Max = int.Parse(groups[3].Value),
                    Min2 = int.Parse(groups[4].Value),
                    Max2 = int.Parse(groups[5].Value)
                });
            }

            var validTixs = new List<string>();

            // Part 1
            var errors = 0;
            foreach (var tix in input.SkipWhile(i => !i.Contains("nearby tickets:")).Skip(1))
            {
                var valid = true;
                foreach (var v in tix.Split(',').Select(int.Parse))
                {
                    if (rules.All(r => !r.IsValid(v)))
                    {
                        valid = false;
                        errors += v;
                    }
                }
                if (valid)
                    validTixs.Add(tix);
            }
            Console.WriteLine($"Part 1 {errors}");

            // Part 2
            var your = input.SkipWhile(i => !i.Contains("your ticket:")).Skip(1).Take(1).First();
            validTixs.Add(your);

            var columns = new Dictionary<int, int[]>();
            for (var i = 0; i < your.Split(',').Length; i++)
            {
                var row = new int[validTixs.Count];
                for (var j = 0; j < row.Length; j++)
                {
                    row[j] = int.Parse(validTixs[j].Split(",")[i]);
                }
                columns[i] = row;
            }

            var pairs = new Dictionary<int, Rule>();
            while (rules.Count > 0)
            {
                foreach (var rule in rules)
                {
                    var matches = 0;
                    var matchColumn = 0;
                    foreach (var (key, value) in columns)
                    {
                        var allValid = true;
                        foreach (var t in value)
                            allValid &= rule.IsValid(t);

                        if (allValid)
                        {
                            matches++;
                            matchColumn = key;
                        }
                    }

                    if (matches == 1)
                    {
                        pairs[matchColumn] = rule;
                        rules.Remove(rule);
                        columns.Remove(matchColumn);
                        break;
                    }
                }
            }

            var yourValues = your.Split(',').Select(int.Parse).ToArray();
            long result = 1;
            foreach (var (col, rule) in pairs)
            {
                if (rule.Field.Contains("departure"))
                {
                    result *= yourValues[col];
                }
            }
            Console.WriteLine($"Part 2 {result}");
        }
    }
}
