using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day6
    {

       public static void Task1()
        {
            Console.WriteLine("AOC2020_Day6_Task1");

            var groups = Day6_Common.GetGroups(Day6_Common.SetOp.Union);

            int count = 0;
            foreach (var group in groups)
                count += group.Count;
           
            Console.WriteLine("Sum :  {0}", count);

        }

       public static void Task2()
        {
            Console.WriteLine("AOC2020_Day6_Task2");
  

            var groups = Day6_Common.GetGroups(Day6_Common.SetOp.Intersect);

            int count = 0;
            foreach (var group in groups)
                count += group.Count;
           
            Console.WriteLine("Sum :  {0}", count);

        }        


    }
}