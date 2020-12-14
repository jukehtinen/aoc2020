using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            // Part 1
            var mem = new Dictionary<ulong, ulong>();
            var mask = string.Empty;
            foreach (var line in input)
            {
                if (line.Contains("mask"))
                {
                    mask = Regex.Match(line, @"mask = (.+)").Groups[1].Value;
                }
                else
                {
                    var match = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");
                    var slot = ulong.Parse(match.Groups[1].Value);
                    var value = ulong.Parse(match.Groups[2].Value);

                    for (var i = 0; i < mask.Length; i++)
                    {
                        if (mask[^(i + 1)] == '0')
                            value &= ~(1UL << i);
                        else if (mask[^(i + 1)] == '1')
                            value |= 1UL << i;
                    }

                    mem[slot] = value;
                }
            }

            ulong sum = 0;
            foreach (var (_, value) in mem)
                sum += value;

            Console.WriteLine($"Part 1 {sum}");

            // Part 2
            mem = new Dictionary<ulong, ulong>();
            mask = string.Empty;
            foreach (var line in input)
            {
                if (line.Contains("mask"))
                {
                    mask = Regex.Match(line, @"mask = (.+)").Groups[1].Value;
                }
                else
                {
                    var match = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");
                    var slot = ulong.Parse(match.Groups[1].Value);
                    var value = ulong.Parse(match.Groups[2].Value);
                    var applied = "";
                    for (var i = 0; i < mask.Length; i++)
                    {
                        if (mask[^(i + 1)] != 'X' && (slot & (1UL << i)) != 0)
                            applied = '1' + applied;
                        else
                            applied = mask[^(i + 1)] + applied;
                    }

                    foreach (var m in GetPermutations(applied))
                    {
                        mem[Convert.ToUInt64(m, 2)] = value;
                    }
                }
            }

            sum = 0;
            foreach (var (_, value) in mem)
                sum += value;

            Console.WriteLine($"Part 2 {sum}");
        }

        private static List<string> GetPermutations(string mask)
        {
            var masks = new List<string>();
            var chars = mask.ToCharArray();
            Swap(masks, chars, Array.IndexOf(chars, 'X'));
            return masks;
        }

        private static void Swap(List<string> masks, char[] chars, in int i)
        {
            if (i == -1)
            {
                masks.Add(new string(chars));
                return;
            }

            chars[i] = '0';
            Swap(masks, chars.ToArray(), Array.IndexOf(chars, 'X', i + 1));
            chars[i] = '1';
            Swap(masks, chars.ToArray(), Array.IndexOf(chars, 'X', i + 1));
        }
    }
}
