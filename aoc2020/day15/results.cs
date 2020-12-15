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
            for (int turn = dict.Count + 1; turn < lastturn; turn++)
            {
                if (dict.ContainsKey(searchnmb))
                {
                    int diff = turn - dict[searchnmb];
                    dict[searchnmb] = turn;
                    searchnmb = diff;
                }
                else
                {
                    dict.Add(searchnmb, turn);
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