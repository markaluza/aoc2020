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
            

            var minwaittime = long.MaxValue;
            long res = 0;

            var buses = lines[1].Split(",");
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

            var sbuses = lines[1].Split(",");
            
            var valpos = new List<(long, long)>();

            for (int i =0; i < sbuses.Length; i++)
            {
                if (sbuses[i] == "x") continue;
                valpos.Add((int.Parse(sbuses[i]), i));
            }


            long time = valpos[0].Item1;
            long mult = time;

            for (int i =1; i < valpos.Count; i++)
            {
                long next = valpos[i].Item1;
                long offset = valpos[i].Item2;

                while ((time + offset) % next !=0)
                    time += mult;

                // nezajimam se kolikrat je tam nasobek cisel ktera uz jsem spocital dulezite jsou modula a ty zustavaji...
                mult *= next;

            }

            Console.WriteLine("Time : {0}", time);

            
            
        }

    }

}