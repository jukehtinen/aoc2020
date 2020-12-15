using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1
            var input = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToList();
            while (input.Count < 2020)
            {
                var index = input.LastIndexOf(input.Last(), input.Count - 2);
                input.Add(index == -1 ? 0 : input.Count - index - 1);
            }
            Console.WriteLine($"Part 1 {input.Last()}");

            // Part 2
            input = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToList();
            var valueToIndex = new Dictionary<int, int>();
            for (var i = 0; i < input.Count - 1; i++)
            {
                valueToIndex[input[i]] = i + 1;
            }

            var currentValue = input.Last();
            for (var i = input.Count; i < 30000000; i++)
            {
                if (valueToIndex.TryGetValue(currentValue, out var val))
                {
                    valueToIndex[currentValue] = i;
                    currentValue = i - val;
                }
                else
                {
                    valueToIndex[currentValue] = i;
                    currentValue = 0;
                }
            }

            Console.WriteLine($"Part 2 {currentValue}");
        }
    }
}
