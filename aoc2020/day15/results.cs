using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{

    class Day15
    {

        static List<int> ReadInput()
        {
            var list = new List<int>();
            var lines = System.IO.File.ReadAllLines(@"./day15/input.txt");
            foreach(var line in lines)
            {
                var nmbs = line.Split(",");
                foreach(var nmb in nmbs)
                {
                    list.Add(int.Parse(nmb));
                }
            }
            return list;
        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day15_Task1");

            var input = ReadInput();
            
            while(true)
            {
                int lastnmb = input.LastOrDefault();
                int lastfound = input.LastIndexOf(lastnmb, input.Count-2);

                if (lastfound < 0)
                {
                    input.Add(0);
                }
                else
                {
                    input.Add(input.Count - lastfound -1 );
                }

                if (input.Count == 2020)
                {
                    Console.WriteLine("Last nmb : {0}", input.LastOrDefault());
                    return;
                }

            }

        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day15_Task1");

            var input =  ReadInput();

            var dict = new Dictionary<int, int>();
            for (int i = 0 ; i < input.Count; i++)
            {
                dict.Add(input[i], i+1);
            }
            
            int searchnmb = 0;
            for (int i = dict.Count; i < 30000000 - 1; i++)
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

            Console.WriteLine("Last nmb : {0}", searchnmb);

        }

    }
}