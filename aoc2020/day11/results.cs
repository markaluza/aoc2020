using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aoc2020
{

    class Day11
    {

        [Serializable]
        class CharMap : List<List<char>>
        {
            public void Load()
            {
                var lines = new List<string>(System.IO.File.ReadAllLines(@"./day11/input.txt"));

                List<List<char>> map = new List<List<char>>();
                foreach(string line in lines)
                {
                    Add( new List<char>(line) );
                }
                
            }   

            public CharMap Clone()
            {
                var copy = new CharMap();
                foreach(var row in this)
                {
                    var rowchars = new List<char>();
                    foreach(var col in row)
                    {
                        rowchars.Add(col);
                    }
                    copy.Add(rowchars);
                }

                return copy;
            }                  

            public bool IsEqual(CharMap map2)
            {

                for (int row = 0; row < Rows(); row++)
                {
                    for (int col = 0; col < Cols(); col++)
                    {
                        if (this[row][col] != map2[row][col])
                            return false;
                    }
                } 
                return true;
            }

     
            bool InBounds(int row, int col)
            {
                return 
                    row >= 0 && row < this.Count &&
                    col >= 0 && col < this[0].Count;
            }       

            public bool IsSeat(int row, int col)
            {
                 return InBounds(row, col) && 
                    (this[row][col] == '#' || this[row][col] == 'L');
            }

            
            public bool IsSeatOccupied(int row, int col)
            {
                if (!InBounds(row, col)) return false;
                return this[row][col] == '#';

            }

            public bool AreSeatsOccupied(int row, int col, int dy, int dx)
            {
                if (!InBounds(row, col)) return false;

                if (IsSeat(row, col))
                {
                    if (this[row][col] == '#') return true;
                    if (this[row][col] == 'L') return false;
                }
                

                return AreSeatsOccupied(row+dy, col+dx, dy, dx);
            }

            public int OccupiedAdjacentSeats1(int row, int col)
            {
                int occ = 0;
                if (IsSeatOccupied(row-1, col-1)) occ++;
                if (IsSeatOccupied(row-1, col)) occ++;
                if (IsSeatOccupied(row-1, col+1)) occ++;
                if (IsSeatOccupied(row, col-1)) occ++;
                if (IsSeatOccupied(row, col+1)) occ++;
                if (IsSeatOccupied(row+1, col-1)) occ++;
                if (IsSeatOccupied(row+1, col)) occ++;
                if (IsSeatOccupied(row+1, col+1)) occ++;
                return occ;
            }

            public int OccupiedAdjacentSeats2(int row, int col)
            {
                int occ = 0;
                if (AreSeatsOccupied(row-1, col-1, -1, -1)) occ++;
                if (AreSeatsOccupied(row-1, col, -1, 0)) occ++;
                if (AreSeatsOccupied(row-1, col+1, -1, +1)) occ++;
                if (AreSeatsOccupied(row, col-1, 0, -1)) occ++;
                if (AreSeatsOccupied(row, col+1, 0, +1)) occ++;
                if (AreSeatsOccupied(row+1, col-1, +1, -1)) occ++;
                if (AreSeatsOccupied(row+1, col, +1, 0)) occ++;
                if (AreSeatsOccupied(row+1, col+1, +1, +1)) occ++;
                return occ;
            }            

            public int Rows() { return Count; }
            public int Cols() { return this[0].Count; }

            public int SumOccupiedSeats()
            {
                int sum =0;
                for (int row = 0; row < Rows(); row++)
                {
                    for (int col = 0; col < Cols(); col++)
                    {
                        if (IsSeatOccupied(row, col))
                        {
                            sum++;
                        }
                    }
                }          
                return sum;
            }

            public void Print()
            {
                 Console.WriteLine("---");

                for (int row = 0; row < Rows(); row++)
                {
                    for (int col = 0; col < Cols(); col++)
                    {
                        Console.Write(this[row][col]);
                    }
                    Console.WriteLine();
                }                   
            }

        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day11_Task1");

            var map =  new CharMap();
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

                //map2.Print();
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

            var map =  new CharMap();
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

                //map2.Print();
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