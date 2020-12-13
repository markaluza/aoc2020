using System;
using System.Collections.Generic;


namespace Aoc2020
{

    class Day13
    {

        public static void Task1()
        {
            
            Console.WriteLine("AOC2020_Day13_Task1");

            var lines = new List<string>(System.IO.File.ReadAllLines(@"./day13/input.txt"));

            long initttime = long.Parse(lines[0]);
            
            var buses = lines[1].Split(",");

            var minwaittime = long.MaxValue;
            long res = 0;

            foreach(var sbus in buses)
            {
                if (sbus == "x") continue;
                var bus = long.Parse(sbus);
                
                long waittime = (bus - initttime % bus)%bus;
                if (waittime < minwaittime)
                {
                    res = waittime * bus;
                    minwaittime = waittime;
                }

            }

            Console.WriteLine("Mult {0}", res);
            
        }     

        public static void Task2()
        {
            
            Console.WriteLine("AOC2020_Day13_Task2");

            var lines = new List<string>(System.IO.File.ReadAllLines(@"./day13/input.txt"));


           // Console.WriteLine("Manhatan Distance {0}", ship.x + ship.y);
            
        }

    }

}