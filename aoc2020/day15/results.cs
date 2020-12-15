using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{

    class Day15
    {

        static Dictionary<int, int> ReadInput()
        {
            var list = new Dictionary<int, int>();
            var lines = System.IO.File.ReadAllLines(@"./day15/input.txt");

            var nmbs = lines[0].Split(","); int cntr = 1;
            foreach(var nmb in nmbs)
            {
                list.Add(int.Parse(nmb), cntr++);
            }

            return list;
        }

        static int LastNmb(int lastturn)
        {
            var dict = ReadInput();       

            int searchnmb = 0;
            for (int i = dict.Count; i < lastturn - 1; i++)
            {
                if (dict.ContainsKey(searchnmb))
                {
                    int diff = i+1 - dict[searchnmb];
                    dict[searchnmb] = i+1;
                    searchnmb = diff;
                }
                else
                {
                    dict.Add(searchnmb, i+1);
                    searchnmb = 0;
                }
            }

            return searchnmb;

        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day15_Task1");
            Console.WriteLine("Last nmb : {0}", LastNmb(2020));

        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day15_Task2");
            Console.WriteLine("Last nmb : {0}", LastNmb(30000000));

        }

    }
}