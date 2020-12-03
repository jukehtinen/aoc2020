using System;
using System.Collections.Generic;
using System.IO;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var map = new bool[lines[0].Length, lines.Length];
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    map[x, y] = lines[y][x] == '#';
                }
            }

            var tests = new List<(int dx, int dy)> { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            long multiplied = 0;
            foreach (var (dx, dy) in tests)
            {
                var posX = 0;
                var posY = 0;
                var trees = 0;
                while (posY + 1 < map.GetLength(1))
                {
                    posX += dx;
                    posY += dy;
                    if (map[posX % map.GetLength(0), posY])
                    {
                        trees++;
                    }
                }
                Console.WriteLine($"{dx}, {dy} -> {trees} trees");
                multiplied = multiplied == 0 ? trees : multiplied * trees;
            }
            Console.WriteLine($"Multiplied {multiplied}");
        }
    }
}
