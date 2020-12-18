using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day18
{
    class Program
    {
        static void Main(string[] args)
        {
            static string Solve(string s)
            {
                while (true)
                {
                    var m = Regex.Match(s, @"^(?'val1'\d+) (?'op'\+|\*) (?'val2'\d+)");
                    if (!m.Success)
                        break;
                    var val = long.Parse(m.Groups["val1"].Value);
                    var val2 = long.Parse(m.Groups["val2"].Value);
                    var r = m.Groups["op"].Value == "*" ? val * val2 : val + val2;
                    s = r + s.Remove(m.Index, m.Length);
                }

                return s;
            }

            static string Solve2(string s)
            {
                foreach (var op in new[] { "+", "*" })
                {
                    while (true)
                    {
                        var m = Regex.Match(s, $"(?'val1'\\d+) (?'op'\\{op}) (?'val2'\\d+)");
                        if (!m.Success)
                            break;
                        var val = long.Parse(m.Groups["val1"].Value);
                        var val2 = long.Parse(m.Groups["val2"].Value);
                        var r = m.Groups["op"].Value == "*" ? val * val2 : val + val2;
                        s = s.Remove(m.Index, m.Length);
                        s = s.Insert(m.Index, r.ToString());
                    }
                }
                return s;
            }

            static long Loop(Func<string, string> solver)
            {
                long sum = 0;
                var input = File.ReadAllLines("input.txt");

                foreach (var line in input)
                {
                    var l = line;
                    var leftIndex = -1;
                    var i = 0;
                    while (l.IndexOfAny(new[] { '(', ')' }) != -1)
                    {
                        switch (l[i])
                        {
                            case '(':
                                leftIndex = i;
                                break;
                            case ')':
                                {
                                    var r = solver(l.Substring(leftIndex + 1, i - leftIndex - 1));
                                    l = l.Remove(leftIndex, i - leftIndex + 1);
                                    l = l.Insert(leftIndex, r);
                                    i = 0;
                                    continue;
                                }
                        }

                        i++;
                    }

                    sum += long.Parse(solver(l));
                }

                return sum;
            }

            Console.WriteLine($"Part 1 {Loop(Solve)}");
            Console.WriteLine($"Part 2 {Loop(Solve2)}");
        }
    }
}
