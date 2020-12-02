using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var valid = 0;
            var validPart2 = 0;
            foreach (var l in lines)
            {
                var match = Regex.Matches(l, @"(\d+)-(\d+) (\w): (\w+)").First().Groups;
                var (min, max, letter, word) = (int.Parse(match[1].Value), int.Parse(match[2].Value), match[3].Value[0], match[4].Value);

                // Part 1
                var letters = word.Count(c => c == letter);
                if (letters >= min && letters <= max)
                {
                    valid++;
                }

                // Part 2
                if ((word[min - 1] == letter || word[max - 1] == letter) &&
                    !(word[min - 1] == letter && word[max - 1] == letter))
                {
                    validPart2++;
                }


            }
            Console.WriteLine(valid);
            Console.WriteLine(validPart2);
        }
    }
}
