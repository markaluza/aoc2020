using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day10
    {

        private static List<int> GetInput()
        {
            var input =  Day1_Common.GetIntInput(@"./day10/input.txt");

            input.Add(0);
            input.Sort();
            input.Add(input[input.Count -1] + 3);

            return input;
        }

        public static void Task1()
        {
            Console.WriteLine("AOC2020_Day10_Task1");

            var input = GetInput();

            Dictionary<int, int> difs = new Dictionary<int, int>();

            for (int i= 0; i < input.Count -1; i++)
            {
                int diff = input[i+1] - input[i];
                if (difs.ContainsKey(diff))
                    difs[diff]++;
                else 
                    difs.Add(diff, 1);
            }

            Console.WriteLine("Mult 1 * 3 : {0}", difs[1] * difs[3]);

        }

       public static void Task2()
        {
            Console.WriteLine("AOC2020_Day10_Task2");

            var input = GetInput();

            int nmbs = 0;
            Int64 combinations = 1;

            for (int i= 1; i < input.Count -1; i++)
            {
                if (input[i+1] - input[i-1] == 2)
                {
                    nmbs++;
                    continue;
                }

                switch(nmbs)
                {
                    case 0: break;
                    case 1: combinations *= 2; break;
                    case 2: combinations *= 4; break;
                    case 3: combinations *= 7; break;
                    default: throw new Exception("dopsat...");
                }               
                nmbs = 0;
                
            } 

            Console.WriteLine("mult : {0}",  combinations);
        }        

    }
}