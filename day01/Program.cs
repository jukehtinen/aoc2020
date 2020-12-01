using System;
using System.IO;
using System.Linq;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllLines("input.txt").Select(l => int.Parse(l));
            // Part 1
            foreach (var n1 in numbers)
            {
                foreach (var n2 in numbers)
                {
                    if (n1 + n2 == 2020)
                    {
                        Console.WriteLine(n1 * n2);
                    }
                }
            }

            // Part 1
            foreach (var n1 in numbers)
            {
                foreach (var n2 in numbers)
                {
                    foreach (var n3 in numbers)
                    {
                        if (n1 + n2 + n3 == 2020)
                        {
                            Console.WriteLine(n1 * n2 * n3);
                        }
                    }
                }
            }

        }
    }
}
