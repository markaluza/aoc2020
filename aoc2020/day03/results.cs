using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day3
    {


       public static void Task1()
        {
            Console.WriteLine("AOC2020_Day3_Task1");

            var Map = new Day3_Common();
            Map.LoadMap();

            int x = 0;
            int y = 0;

            int trees = 0;

            while(!Map.IsLastLine(y))
            {
                if (Map.IsTree(x,y)) trees++;
                x += 3;
                y += 1;
            }
  
            Console.WriteLine("Encoutered trees : {0}", trees);

        }

       public static void Task2()
        {
            Console.WriteLine("AOC2020_Day3_Task2");

            var Map = new Day3_Common();
            Map.LoadMap();

            List<(int, int)> slopes =  new List<(int, int)>{ 
                (1,1), 
                (3,1),
                (5,1),
                (7,1),
                (1,2)
                };

            Int64 mult = 1;

            foreach (var s in slopes)
            {
                int x = 0;
                int y = 0;

                int trees = 0;

                while(!Map.IsLastLine(y))
                {
                    if (Map.IsTree(x,y)) trees++;
                    x += s.Item1;
                    y += s.Item2;
                }
    
                Console.WriteLine("({0}, {1}) - Encoutered trees : {2}", s.Item1, s.Item2, trees);
                mult *= trees;

            }

            Console.WriteLine("Mult : {0}", mult);

            


        }        


    }
}