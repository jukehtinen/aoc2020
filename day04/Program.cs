using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            // Part 1
            var reqs = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var current = "";
            var valids = new List<Dictionary<string, string>>();
            for (var i = 0; i < lines.Length + 1; i++)
            {
                if (i >= lines.Length || string.IsNullOrEmpty(lines[i]))
                {
                    var props = current.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .ToDictionary(x => x.Split(":")[0], x => x.Split(":")[1]);
                    if (reqs.All(k => props.ContainsKey(k)))
                    {
                        valids.Add(props);
                    }
                    current = "";
                }
                else
                {
                    current += $" {lines[i].Replace("\n", "")}";
                }
            }
            Console.WriteLine(valids.Count);

            // Part 2
            var validated = 0;
            foreach (var v in valids)
            {
                // byr (Birth Year) - four digits; at least 1920 and at most 2002.
                if (int.Parse(v["byr"]) < 1920 || int.Parse(v["byr"]) > 2002)
                    continue;

                // iyr(Issue Year) - four digits; at least 2010 and at most 2020.
                if (int.Parse(v["iyr"]) < 2010 || int.Parse(v["iyr"]) > 2020)
                    continue;

                // eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
                if (int.Parse(v["eyr"]) < 2020 || int.Parse(v["eyr"]) > 2030)
                    continue;

                // hgt(Height) - a number followed by either cm or in: If cm, the number must be at least 150 and at most 193.
                //  If in, the number must be at least 59 and at most 76.
                var match = Regex.Matches(v["hgt"], @"(\d+)(cm|in)");
                if (match.Any())
                {
                    var (h, unit) = (int.Parse(match.First().Groups[1].Value), match.First().Groups[2].Value);
                    switch (unit)
                    {
                        case "cm" when (h < 150 || h > 193):
                        case "in" when (h < 59 || h > 76):
                            continue;
                    }
                }
                else
                    continue;


                // hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                if (!Regex.IsMatch(v["hcl"], @"#[0-9 a-f]{6}"))
                    continue;

                // ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                if (!Regex.IsMatch(v["ecl"], "(amb|blu|brn|gry|grn|hzl|oth)"))
                    continue;

                // pid(Passport ID) - a nine - digit number, including leading zeroes.
                if (!Regex.IsMatch(v["pid"], @"^\d{9}$"))
                    continue;

                validated++;
            }

            Console.WriteLine(validated);
        }
    }
}
