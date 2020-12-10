using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day9
    {

        public static void Task1()
        {
            Console.WriteLine("AOC2020_Day9_Task1");

            var input = Day9_Common.GetInput();

            const int preamblelen = 25;

            List<Int64> preamble = new List<Int64>();
            for (int i =0; i < input.Count; i++)
            {
                Int64 val = input[i];
                if (i < preamblelen) 
                {
                    preamble.Add(val);
                    continue;
                }

                if (Day9_Common.ValidNmb(preamble, val))
                {
                    preamble[i%preamblelen] = val;
                    continue;
                }

                Console.WriteLine("Nmb : {0}", val);
                return;

            }

            Console.WriteLine("Not found????");

        }

        public static void Task2()
        {
          
            Console.WriteLine("AOC2020_Day9_Task2");

            var input = Day9_Common.GetInput();

            var invalidnmb = 14144619;

            for (int i =0; i < input.Count-1; i++)
            {
                var sum = input[i];
                int j = i+1;
                for (; j < input.Count &&  sum < invalidnmb; j++)
                {
                    sum += input[j];
                }
                if (sum == invalidnmb)
                {
                    var list = new SortedSet<Int64>(input.GetRange(i, j-i+1));
                    Console.WriteLine("Min : {0} Max : {1} Sum : {2}", list.Min, list.Max, list.Min + list.Max);
                    return;
                }
            }

            Console.WriteLine("Not found????");
        }      
    }
}