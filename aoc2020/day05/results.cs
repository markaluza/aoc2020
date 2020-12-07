using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day5
    {

       public static void Task1()
        {
            Console.WriteLine("AOC2020_Day5_Task1");

            var ids = Day5_Common.GetSeatIds();
            int maxseatId = 0;
            foreach (var seatId in ids)
            {
                maxseatId = Math.Max(maxseatId, seatId);
            }

            Console.WriteLine("MaxSeatId :  {0}", maxseatId);

        }

       public static void Task2()
        {
            Console.WriteLine("AOC2020_Day5_Task2");
            
            var ids = Day5_Common.GetSeatIds();

            bool [] occupied = new bool[1024];
            for (int i =0; i < 1024; i++) occupied[i] = false;
            foreach (var seatid in ids) occupied[seatid] = true;

            for (int i =1; i < 1024; i++)
            {
                if (occupied[i-1] && !occupied[i] && occupied[i+1]) 
                {
                    Console.WriteLine("Free SeatId :  {0}", i);
                    return;
                }
            }

  
            Console.WriteLine("?????");



        }        


    }
}