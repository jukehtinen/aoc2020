using System;
using System.IO;
using System.Linq;

namespace day09
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

            var preambleSize = 25;
            var numbers = input[..preambleSize].ToList();

            // Part 1
            long answer = 0;
            for (var i = preambleSize; i < input.Length; i++)
            {
                var found = false;
                foreach (var a in numbers)
                    foreach (var b in numbers)
                        found |= a != b && a + b == input[i];

                if (found)
                {
                    numbers.Add(input[i]);
                    numbers.RemoveAt(0);
                }
                else
                {
                    answer = input[i];
                    break;
                }
            }
            Console.WriteLine($"Part 1 {answer}");

            // Part 2
            for (var i = 0; i < input.Length; i++)
            {
                long sum = 0;
                var j = i;
                while (sum < answer)
                    sum += input[j++];

                if (sum == answer)
                {
                    var range = input[i..j];
                    Console.WriteLine($"Part 2 {range.Min() + range.Max()}");
                    break;
                }
            }
        }
    }
}
