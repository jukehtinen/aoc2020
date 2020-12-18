using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            // Part 1
            {
                var active = new List<(int x, int y, int z)>();
                for (var y = 0; y < input.Length; y++)
                    for (var x = 0; x < input[y].Length; x++)
                        if (input[y][x] == '#')
                            active.Add((x, y, 0));

                for (var i = 0; i < 6; i++)
                {
                    var next = new List<(int x, int y, int z)>();
                    var boundMin = (x: active.Min(a => a.x) - 1, y: active.Min(a => a.y) - 1, z: active.Min(a => a.z) - 1);
                    var boundMax = (x: active.Max(a => a.x) + 1, y: active.Max(a => a.y) + 1, z: active.Max(a => a.z) + 1);

                    for (var z = boundMin.z; z <= boundMax.z; z++)
                        for (var y = boundMin.y; y <= boundMax.y; y++)
                            for (var x = i; x <= boundMax.x; x++)
                            {
                                var isActive = active.Any(a => a.x == x && a.y == y && a.z == z);
                                var actives = active.Count(a =>
                                    a.z >= z - 1 && a.z <= z + 1 && a.y >= y - 1 && a.y <= y + 1 && a.x >= x - 1 &&
                                    a.x <= x + 1);
                                if (isActive)
                                    actives--;

                                if (isActive)
                                {
                                    if (actives == 2 || actives == 3)
                                        next.Add((x, y, z));
                                }
                                else
                                {
                                    if (actives == 3)
                                        next.Add((x, y, z));
                                }
                            }

                    active = next;
                }

                Console.WriteLine($"Part 1 {active.Count}");
            }

            // Part 2
            {
                var active = new List<(int x, int y, int z, int w)>();
                for (var y = 0; y < input.Length; y++)
                    for (var x = 0; x < input[y].Length; x++)
                        if (input[y][x] == '#')
                            active.Add((x, y, 0, 0));

                for (var i = 0; i < 6; i++)
                {
                    var next = new List<(int x, int y, int z, int w)>();
                    var boundMin = (x: active.Min(a => a.x) - 1, y: active.Min(a => a.y) - 1, z: active.Min(a => a.z) - 1, w: active.Min(a => a.w) - 1);
                    var boundMax = (x: active.Max(a => a.x) + 1, y: active.Max(a => a.y) + 1, z: active.Max(a => a.z) + 1, w: active.Max(a => a.w) + 1);

                    for (var w = boundMin.z; w <= boundMax.z; w++)
                        for (var z = boundMin.z; z <= boundMax.z; z++)
                            for (var y = boundMin.y; y <= boundMax.y; y++)
                                for (var x = boundMin.x; x <= boundMax.x; x++)
                                {
                                    var isActive = active.Any(a => a.x == x && a.y == y && a.z == z && a.w == w);
                                    var actives = active.Count(a =>
                                        a.z >= z - 1 && a.z <= z + 1 && a.y >= y - 1 && a.y <= y + 1 && a.x >= x - 1 &&
                                        a.x <= x + 1 && a.w >= w - 1 && a.w <= w + 1);
                                    if (isActive)
                                        actives--;

                                    if (isActive)
                                    {
                                        if (actives == 2 || actives == 3)
                                            next.Add((x, y, z, w));
                                    }
                                    else
                                    {
                                        if (actives == 3)
                                            next.Add((x, y, z, w));
                                    }
                                }

                    active = next;
                }

                Console.WriteLine($"Part 2 {active.Count}");
            }
        }
    }
}
