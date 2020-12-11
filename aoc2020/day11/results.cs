using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day11
    {
        
        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day11_Task1");

            var map =  new SeatMap();
            map.Load();

            var map2 = map.Clone();

            while(true)
            {

                for (int row = 0; row < map2.Rows(); row++)
                {
                    for (int col = 0; col < map2.Cols(); col++)
                    {
                        if (!map.IsSeat(row, col)) continue;

                        if (!map.IsSeatOccupied(row, col) && map.OccupiedAdjacentSeats1(row, col) == 0)
                        {
                            map2[row][col] = '#';
                        }
                        else if (map.IsSeatOccupied(row, col) && map.OccupiedAdjacentSeats1(row, col) >= 4)
                        {
                            map2[row][col] = 'L';
                        }
                    }
                }

                if (map.IsEqual(map2))
                {
                    break;
                }

                map = map2.Clone();

            }

            Console.WriteLine("Occupied seats {0}", map.SumOccupiedSeats());
            
        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day11_Task2");

            var map =  new SeatMap();
            map.Load();

            var map2 = map.Clone();

            while(true)
            {

                for (int row = 0; row < map2.Rows(); row++)
                {
                    for (int col = 0; col < map2.Cols(); col++)
                    {
                        if (!map.IsSeat(row, col)) continue;

                        if (!map.IsSeatOccupied(row, col) && map.OccupiedAdjacentSeats2(row, col) == 0)
                        {
                            map2[row][col] = '#';
                        }
                        else if (map.IsSeatOccupied(row, col) && map.OccupiedAdjacentSeats2(row, col) >= 5)
                        {
                            map2[row][col] = 'L';
                        }
                    }
                }

                if (map.IsEqual(map2))
                {
                    break;
                }

                map = map2.Clone();

            }

            Console.WriteLine("Occupied seats {0}", map.SumOccupiedSeats());
            
        }


    }
}