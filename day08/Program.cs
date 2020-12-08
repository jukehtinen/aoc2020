using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day08
{
    struct Instruction
    {
        public string Op { get; set; }
        public int Param { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var instructions = new List<Instruction>();
            foreach (var l in lines)
            {
                var tok = l.Split(" ");
                instructions.Add(new Instruction { Op = tok[0], Param = int.Parse(tok[1]) });
            }

            // Part 1
            var counts = new int[instructions.Count];
            var pc = 0;
            var acc = 0;
            while (true)
            {
                counts[pc]++;
                if (counts[pc] > 1)
                {
                    Console.WriteLine($"Part 1 {acc}");
                    break;
                }

                switch (instructions[pc].Op)
                {
                    case "nop":
                        pc++;
                        break;
                    case "acc":
                        acc += instructions[pc].Param;
                        pc++;
                        break;
                    case "jmp":
                        pc += instructions[pc].Param;
                        break;
                }
            }

            // Part 2
            for (var i = 0; i < instructions.Count; i++)
            {
                if (instructions[i].Op == "acc")
                    continue;

                var modInstructions = instructions.ToList();
                modInstructions[i] = new Instruction
                {
                    Op = instructions[i].Op == "nop" ? "jmp" : "nop",
                    Param = instructions[i].Param
                };

                pc = 0;
                acc = 0;
                counts = new int[instructions.Count];
                while (true)
                {
                    if (pc >= modInstructions.Count)
                    {
                        Console.WriteLine($"Part 2 {acc}");
                        break;
                    }

                    counts[pc]++;
                    if (counts[pc] > 1)
                    {
                        break;
                    }

                    switch (modInstructions[pc].Op)
                    {
                        case "nop":
                            pc++;
                            break;
                        case "acc":
                            acc += modInstructions[pc].Param;
                            pc++;
                            break;
                        case "jmp":
                            pc += modInstructions[pc].Param;
                            break;
                    }
                }
            }
        }
    }
}
