using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var width = input[0].Length;
            var height = input.Length;
            var data = string.Join("", input).Replace("\n", "");

            // Part 1
            int GetOccupied(string map, int x0, int y0, int x1, int y1)
            {
                var occupied = 0;
                for (var y = y0; y < y1; y++)
                    for (var x = x0; x < x1; x++)
                        if (x >= 0 && y >= 0 && x < width && y < height && map[y * width + x] == '#')
                            occupied++;
                return occupied;
            }

            while (true)
            {
                var next = "";
                for (var i = 0; i < width * height; i++)
                {
                    var x = i % width;
                    var y = i / width;
                    next += data[i] switch
                    {
                        'L' when GetOccupied(data, x - 1, y - 1, x + 2, y + 2) == 0 => "#",
                        '#' when GetOccupied(data, x - 1, y - 1, x + 2, y + 2) >= 5 => "L",
                        _ => data[i]
                    };
                }

                if (data.Count(c => c == '#') == next.Count(c => c == '#'))
                {
                    Console.WriteLine($"Part 1 {data.Count(c => c == '#')}");
                    break;
                }

                data = next;
            }

            // Part 2
            int GetOccupied2(string map, int index)
            {
                var steps = new List<(int dx, int dy)> { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1) };
                var occupied = 0;
                foreach (var (dx, dy) in steps)
                {
                    var x = index % width + dx;
                    var y = index / width + dy;
                    while (x >= 0 && y >= 0 && x < width && y < height)
                    {
                        if (map[y * width + x] == '#')
                        {
                            occupied++;
                            break;
                        }
                        if (map[y * width + x] == 'L')
                        {
                            break;
                        }
                        x += dx;
                        y += dy;
                    }
                }

                return occupied;
            }

            data = string.Join("", input).Replace("\n", "");

            while (true)
            {
                var next = "";
                for (var i = 0; i < width * height; i++)
                {
                    next += data[i] switch
                    {
                        'L' when GetOccupied2(data, i) == 0 => '#',
                        '#' when GetOccupied2(data, i) >= 5 => 'L',
                        _ => data[i]
                    };
                }

                if (data.Count(c => c == '#') == next.Count(c => c == '#'))
                {
                    Console.WriteLine($"Part 2 {data.Count(c => c == '#')}");
                    break;
                }

                data = next;
            }
        }
    }
}
