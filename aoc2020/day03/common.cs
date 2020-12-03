using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day3_Common
    {

        private List<string> Map = null;
        
        public void LoadMap()
        {

            if (Map != null) return;

            var logFile = System.IO.File.ReadAllLines(@"..\input_3.i");
            Map = new List<string>(logFile);
        }

        public bool IsTree(int x, int y)
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

        public bool IsLastLine(int y)
        {
            return y >= Map.Count;
        }
    
    }
}