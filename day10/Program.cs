using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToList();
            input.Add(0);
            input.Add(input.Max() + 3);
            var adapters = input.ToArray();

            // Part 1
            var jolts = 0;
            var diff1 = 0;
            var diff3 = 0;
            var possible = adapters.Where(a => a <= jolts + 3 && a > jolts).ToArray();
            while (possible.Any())
            {
                var d = possible.Min() - jolts;
                if (d == 1) diff1++;
                if (d == 3) diff3++;
                jolts = possible.Min();
                possible = adapters.Where(a => a <= jolts + 3 && a > jolts).ToArray();
            }
            Console.WriteLine($"Diff 1 {diff1} Diff 3 {diff3} Res {diff1 * diff3}");

            // Part 2
            adapters = adapters.OrderBy(a => a).ToArray();
            var paths = adapters.ToDictionary(a => a, a => (long)0);
            paths[0] = 1; // one path to get to first one
            foreach (var i in adapters)
            {
                possible = adapters.Where(a => a <= i + 3 && a > i).ToArray();
                foreach (var p in possible)
                    paths[p] += paths[i]; // add parent paths to child paths
            }

            Console.WriteLine(paths.Last().Value);
        }
    }
}
