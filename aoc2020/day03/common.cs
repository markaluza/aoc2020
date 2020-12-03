using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day3_Common
    {

        private static List<string> Map = null;
        
        public static void LoadMap()
        {

            if (Map != null) return;

            var logFile = System.IO.File.ReadAllLines(@"..\input_3.i");
            Map = new List<string>(logFile);
        }

        public static bool IsTree(int x, int y)
        {
            if (y > Map.Count)
            {
                System.Diagnostics.Debug.Assert(false);
                return false;
            }

            if (x >= Map[y].Length) 
                x = (x % Map[y].Length);
            
            return Map[y][x] == '#';

        }

        public static bool IsLastLine(int y)
        {
            return y >= Map.Count;
        }
    
    }
}